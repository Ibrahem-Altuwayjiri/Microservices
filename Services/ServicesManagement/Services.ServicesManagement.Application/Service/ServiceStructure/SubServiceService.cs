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
    public class SubServiceService : BaseService, ISubServiceService
    {
        public SubServiceService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor, mapper)
        {
        }

        public async Task<SubServiceDto> create(CreateSubServiceDto dto)
        {
            var model = _mapper.Map<SubService>(dto);
            var result = await _unitOfWork.SubServiceRepository.Add(model);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<SubServiceDto>(result);
        }
        public async Task<SubServiceDto> update(UpdateSubServiceDto dto)
        {
            if (dto.Id == null)
                throw new RestfulException("Id is required", RestfulStatusCodes.BadRequest);

            // Load existing tracked entity from the database
            var entity = await _unitOfWork.SubServiceRepository.FindOneOrDefault(a => a.Id == dto.Id);
            if (entity == null)
                throw new RestfulException("Not Found sub service", RestfulStatusCodes.NotFound);

            // Map incoming DTO onto the tracked entity to update mutable fields only
            _mapper.Map(dto, entity);

            var result = await _unitOfWork.SubServiceRepository.Update(entity);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<SubServiceDto>(result);
        }

        public async Task<bool> activate(string id)
        {
            var entity = await _unitOfWork.SubServiceRepository.FindOneOrDefault(m => m.Id == id );
            if (entity == null)
                throw new RestfulException("Not Found sub service", RestfulStatusCodes.NotFound);
            if (entity.IsActive)
                throw new RestfulException("The sub service is already active", RestfulStatusCodes.BadRequest);


            entity.IsActive = true;
            await _unitOfWork.SubServiceRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<bool> deactivate(string id)
        {
            var entity = await _unitOfWork.SubServiceRepository.FindOneOrDefault(m => m.Id == id );
            if (entity == null)
                throw new RestfulException("Not Found sub service", RestfulStatusCodes.NotFound);

            if (!entity.IsActive)
                throw new RestfulException("The sub service is already inactive", RestfulStatusCodes.BadRequest);

            entity.IsActive = false;
            await _unitOfWork.SubServiceRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<List<SubServiceDto>> getAll(string filter = "", PaginationParametersDto? Pagination = null)
        {
            if (filter != null)
                filter = filter.ToLower();

            var entities = await _unitOfWork.SubServiceRepository.FindWithInclude(m => m.IsActive && (
                                                                                string.IsNullOrEmpty(filter) ||
                                                                                m.NameAr.ToLower().Contains(filter) ||
                                                                                m.NameEn.ToLower().Contains(filter)
                                                                                ), _mapper.Map<PaginationParameters>(Pagination),
                                                                                o => o.MainService);
            return _mapper.Map<List<SubServiceDto>>(entities);
        }

        public async Task<List<SubServiceWithAuditDto>> getAllWithAudit(string filter = "", PaginationParametersDto? Pagination = null)
        {
            if (filter != null)
                filter = filter.ToLower();

            var entities = await _unitOfWork.SubServiceRepository.FindWithInclude(m => (
                                                                                string.IsNullOrEmpty(filter) ||
                                                                                m.NameAr.ToLower().Contains(filter) ||
                                                                                m.NameEn.ToLower().Contains(filter)
                                                                                ), _mapper.Map<PaginationParameters>(Pagination),
                                                                                o => o.MainService);
            return _mapper.Map<List<SubServiceWithAuditDto>>(entities);
        }

        public async Task<SubServiceDto> getById(string id)
        {
            var entity = await _unitOfWork.SubServiceRepository.FindOneOrDefaultWithInclude(m => m.Id == id && m.IsActive, o => o.MainService);
            if (entity == null)
                throw new RestfulException("Not Found sub service", RestfulStatusCodes.NotFound);

            return _mapper.Map<SubServiceDto>(entity);
        }

        public async Task<SubServiceDto> update(SubServiceDto dto)
        {
            if (dto == null)
                throw new RestfulException("Id is required", RestfulStatusCodes.BadRequest);

            var model = _mapper.Map<SubService>(dto);
            var result = await _unitOfWork.SubServiceRepository.Update(model);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<SubServiceDto>(result);
        }
    }
}
