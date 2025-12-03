using AutoMapper;
using Microsoft.AspNetCore.Http;
using Services.ServicesManagement.Application.IService.ServiceStructure;
using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using Services.ServicesManagement.Application.Service.Abstract;
using Services.ServicesManagement.Domain.Entities.ServiceStructure;
using Services.ServicesManagement.Domain.IRepositories;
using Services.ServicesManagement.Domain.Pagination;
using Services.ServicesManagement.Infrastructure.Configuration.ExceptionHandlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.Service.ServiceStructure
{
    public class SubSubServiceService : BaseService, ISubSubServiceService
    {
        public SubSubServiceService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor, mapper)
        {
        }

        public async Task<SubSubServiceDto> create(CreateSubSubServiceDto dto)
        {
            var model = _mapper.Map<SubSubService>(dto);
            var result = await _unitOfWork.SubSubServiceRepository.Add(model);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<SubSubServiceDto>(result);
        }
        public async Task<SubSubServiceDto> update(UpdateSubSubServiceDto dto)
        {
            if (dto.Id == null)
                throw new RestfulException("Id is required", RestfulStatusCodes.BadRequest);

            // Load existing tracked entity from the database
            var entity = await _unitOfWork.SubSubServiceRepository.FindOneOrDefault(a => a.Id == dto.Id);
            if (entity == null)
                throw new RestfulException("Not Found sub-sub service", RestfulStatusCodes.NotFound);

            // Map incoming DTO onto the tracked entity to update mutable fields only
            _mapper.Map(dto, entity);

            var result = await _unitOfWork.SubSubServiceRepository.Update(entity);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<SubSubServiceDto>(result);
        }

        public async Task<bool> activate(string id)
        {
            var entity = await _unitOfWork.SubSubServiceRepository.FindOneOrDefault(m => m.Id == id );
            if (entity == null)
                throw new RestfulException("Not Found sub-sub service", RestfulStatusCodes.NotFound);

            if (entity.IsActive)
                throw new RestfulException("The sub-sub service is already active", RestfulStatusCodes.BadRequest);

            entity.IsActive = true;
            await _unitOfWork.SubSubServiceRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<bool> deactivate(string id)
        {
            var entity = await _unitOfWork.SubSubServiceRepository.FindOneOrDefault(m => m.Id == id);
            if (entity == null)
                throw new RestfulException("Not Found sub-sub service", RestfulStatusCodes.NotFound);

            if (!entity.IsActive)
                throw new RestfulException("The sub-sub service is already inactive", RestfulStatusCodes.BadRequest);

            entity.IsActive = false;
            await _unitOfWork.SubSubServiceRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<List<SubSubServiceDto>> getAll(string filter = "", PaginationParametersDto? Pagination = null)
        {
            if (filter != null)
                filter = filter.ToLower();

            var entities = await _unitOfWork.SubSubServiceRepository.FindWithInclude(m => m.IsActive && (
                                                                                string.IsNullOrEmpty(filter) ||
                                                                                m.NameAr.ToLower().Contains(filter) ||
                                                                                m.NameEn.ToLower().Contains(filter)
                                                                                ), _mapper.Map<PaginationParameters>(Pagination),
                                                                                o => o.SubService);
            return _mapper.Map<List<SubSubServiceDto>>(entities);
        }

        public async Task<List<SubSubServiceWithAuditDto>> getAllWithAudit(string filter = "", PaginationParametersDto? Pagination = null)
        {
            if (filter != null)
                filter = filter.ToLower();

            var entities = await _unitOfWork.SubSubServiceRepository.FindWithInclude(m =>  (
                                                                                string.IsNullOrEmpty(filter) ||
                                                                                m.NameAr.ToLower().Contains(filter) ||
                                                                                m.NameEn.ToLower().Contains(filter)
                                                                                ), _mapper.Map<PaginationParameters>(Pagination),
                                                                                o => o.SubService);
            return _mapper.Map<List<SubSubServiceWithAuditDto>>(entities);
        }

        public async Task<SubSubServiceDto> getById(string id)
        {
            var entity = await _unitOfWork.SubSubServiceRepository.FindOneOrDefaultWithInclude(m => m.Id == id && m.IsActive, o => o.SubService);
            if (entity == null)
                throw new RestfulException("Not Found sub-sub service", RestfulStatusCodes.NotFound);

            return _mapper.Map<SubSubServiceDto>(entity);
        }

       
    }
}
