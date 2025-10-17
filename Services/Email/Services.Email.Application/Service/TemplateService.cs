using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.Email.Application.IService;
using Services.Email.Application.Models.Abstract;
using Services.Email.Application.Models.Dto.Template;
using Services.Email.Application.Models.Dto.TemplateDetails;
using Services.Email.Domain.Entities;
using Services.Email.Domain.IRepositories;
using Services.Email.Infrastructure.Configuration.ExceptionHandlers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Service
{
    public class TemplateService : ITemplateService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TemplateService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TemplateDto> CreateTemplate(CreateTemplateDto createTemplate)
        {
            var model = _mapper.Map<Template>(createTemplate);
            var template = await _unitOfWork.TemplateRepository.Add(model);

            var templateDetails = _mapper.Map<TemplateDetails>(createTemplate.TemplateDetails);

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim?.Subject?.Claims.FirstOrDefault(u => u.Properties.Values.Any(x => x.Equals("sub")))?.Value;


            templateDetails.CreateBy = userId;
            templateDetails.TemplateId = template.Id;
            await _unitOfWork.TemplateDetailsRepository.Add(templateDetails);
            await _unitOfWork.CompletedAsync();

            var response = _mapper.Map<TemplateDto>(template);
            response.templateDetails = _mapper.Map<TemplateDetailsDto>(await _unitOfWork.TemplateDetailsRepository.FindOneOrDefault(m => m.TemplateId == template.Id && m.IsActive));

            return response;
        }

        public async Task<IEnumerable<TemplateDto>> GetAllTemplates()
        {
            IEnumerable<Template> templates = await _unitOfWork.TemplateRepository.FindWithInclude(m =>
                                                                                            m.TemplateDetails.Any(td => td.IsActive && td.VersionNumber == m.VersionNumber)
                                                                                            , o => o.TemplateDetails);
            return _mapper.Map<IEnumerable<TemplateDto>>(templates);
        }

        public async Task<TemplateDto> GetTemplate(int id)
        {
            Template? template = await _unitOfWork.TemplateRepository.FindOneOrDefaultWithInclude(m => m.Id == id &&
                                                                                            m.TemplateDetails.Any(td => td.IsActive && td.VersionNumber == m.VersionNumber)
                                                                                            , o => o.TemplateDetails);
            if(template == null)
                throw new RestfulException("Not Found Template", RestfulStatusCodes.NotFound);


            return _mapper.Map<TemplateDto>(template);
        }

        public async Task<TemplateDto> UpdateTemplate(UpdateTemplateDto updateTemplate)
        {
            Template template = await _unitOfWork.TemplateRepository.FindOneOrDefault(m => m.Id == updateTemplate.Id );
            if (template == null)
                throw new RestfulException("Not Found Template", RestfulStatusCodes.NotFound);

            template.Name = updateTemplate.Name;
            template.VersionNumber += 1;

            await _unitOfWork.TemplateRepository.Update(template);

            var newTemplateDetails = _mapper.Map<TemplateDetails>(updateTemplate.TemplateDetails);
            newTemplateDetails.VersionNumber = template.VersionNumber;
            newTemplateDetails.TemplateId = template.Id;

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim?.Subject?.Claims.FirstOrDefault(u => u.Properties.Values.Any(x => x.Equals("sub")))?.Value;

            newTemplateDetails.CreateBy = userId;

            //deactivate old template details
            var oldTemplateDetails = await _unitOfWork.TemplateDetailsRepository.Find(m => m.TemplateId == template.Id && m.IsActive);
            foreach (var item in oldTemplateDetails)
            {
                item.IsActive = false;
                await _unitOfWork.TemplateDetailsRepository.Update(item);
            }

            await _unitOfWork.TemplateDetailsRepository.Add(newTemplateDetails);
            await _unitOfWork.CompletedAsync(); // SaveChanges (update template and add template details)

            var response = _mapper.Map<TemplateDto>(template);
            response.templateDetails = _mapper.Map<TemplateDetailsDto>(newTemplateDetails);

            return response;

        }
    }
}
