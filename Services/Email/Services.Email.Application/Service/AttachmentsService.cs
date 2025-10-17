using AutoMapper;
using Microsoft.AspNetCore.Http;
using Services.Email.Application.IService;
using Services.Email.Application.Models.Dto.Attachments;
using Services.Email.Domain.Entities;
using Services.Email.Domain.IRepositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Service
{
    public class AttachmentsService : IAttachmentsService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AttachmentsService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<AttachmentsDto>> Create(List<CreateAttachmentsDto> createAttachments)
        {
            var Attachments = _mapper.Map<List<Attachments>>(createAttachments);
            Attachments = await _unitOfWork.AttachmentsRepository.AddRange(Attachments); 
            await _unitOfWork.CompletedAsync();

            return _mapper.Map<IEnumerable<AttachmentsDto>>(Attachments);

        }

        public async Task<IEnumerable<SendAttachmentsDto>> GetAttachments(int EmailDetailsId)
        {
            var attachments = (await _unitOfWork.AttachmentsRepository.Find(m => m.EmailDetails.Id == EmailDetailsId));
            List<SendAttachmentsDto> sendattachments = new List<SendAttachmentsDto>();
            foreach (var attachment in attachments)
            {
                var attachContent = new byte[64]; //TODO: get attachContent by file managment service using ReferenceId
                sendattachments.Add(new SendAttachmentsDto
                {
                    FileBytes = attachContent,
                    FileName = attachment.Name
                });
            }
            return sendattachments;
        }

        public async Task<AttachmentsDto> GetById(int Id)
        {
            var attachments = await _unitOfWork.AttachmentsRepository.FindOneOrDefaultWithInclude(m => m.Id == Id, o => o.EmailDetails);
            return _mapper.Map<AttachmentsDto>(attachments);
        }

        public async Task<AttachmentsDto> GetByReferenceId(string ReferenceId)
        {
            var attachments = await _unitOfWork.AttachmentsRepository.FindOneOrDefaultWithInclude(m => m.ReferenceId == ReferenceId, o => o.EmailDetails);
            return _mapper.Map<AttachmentsDto>(attachments);
        }
    }
}
