using AutoMapper;
using Microsoft.AspNetCore.Http;
using Services.ServicesManagement.Application.IService.ServiceStructure;
using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure.CreateOrUpdate;
using Services.ServicesManagement.Application.Service.Abstract;
using Services.ServicesManagement.Domain.Entities.ServiceStructure;
using Services.ServicesManagement.Domain.IRepositories;
using Services.ServicesManagement.Domain.Pagination;
using Services.ServicesManagement.Infrastructure.Configuration.ExceptionHandlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.Service.ServiceStructure
{
    public class MainServiceService : BaseService, IMainServiceService
    {
        public MainServiceService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor, mapper)
        {
        }

        public async Task<MainServiceDto> create(CreateMainServiceDto dto)
        {
            var model = _mapper.Map<MainService>(dto);
            var result = await _unitOfWork.MainServiceRepository.Add(model);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<MainServiceDto>(result);
        }
        public async Task<MainServiceDto> update(UpdateMainServiceDto dto)
        {
            if (dto.Id == null)
                throw new RestfulException("Id is required", RestfulStatusCodes.BadRequest);

            // Load existing tracked entity from the database
            var entity = await _unitOfWork.MainServiceRepository.FindOneOrDefault(a => a.Id == dto.Id);
            if (entity == null)
                throw new RestfulException("Not Found main service", RestfulStatusCodes.NotFound);

            // Map incoming DTO onto the tracked entity to update mutable fields only
            _mapper.Map(dto, entity);

            var result = await _unitOfWork.MainServiceRepository.Update(entity);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<MainServiceDto>(result);
        }

        public async Task<bool> activate(string id)
        {
            var entity = await _unitOfWork.MainServiceRepository.FindOneOrDefault(m => m.Id == id);
            if (entity == null)
                throw new RestfulException("Not Found main service", RestfulStatusCodes.NotFound);

            if (entity.IsActive)
                throw new RestfulException("The main service is already inactive", RestfulStatusCodes.BadRequest);

            entity.IsActive = true;
            await _unitOfWork.MainServiceRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<bool> deactivate(string id)
        {
            var entity = await _unitOfWork.MainServiceRepository.FindOneOrDefault(m => m.Id == id );
            if (entity == null)
                throw new RestfulException("Not Found main service", RestfulStatusCodes.NotFound);

            if (!entity.IsActive)
                throw new RestfulException("The main service is already inactive", RestfulStatusCodes.BadRequest);

            entity.IsActive = false;
            await _unitOfWork.MainServiceRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<List<MainServiceDto>> getAll(string filter = "", PaginationParametersDto? Pagination = null)
        {
            if(filter != null)
                filter = filter.ToLower();

            var entities = await _unitOfWork.MainServiceRepository.Find(m => m.IsActive && (
                                                                                string.IsNullOrEmpty(filter) ||
                                                                                m.NameAr.ToLower().Contains(filter) ||
                                                                                m.NameEn.ToLower().Contains(filter) 
                                                                                ), _mapper.Map<PaginationParameters>(Pagination));
            return _mapper.Map<List<MainServiceDto>>(entities);
        }

        public async Task<List<MainServiceWithAuditDto>> getAllWithAudit(string filter = "", PaginationParametersDto? Pagination = null)
        {
            if (filter != null)
                filter = filter.ToLower();

            var entities = await _unitOfWork.MainServiceRepository.Find(m =>  (
                                                                                string.IsNullOrEmpty(filter) ||
                                                                                m.NameAr.ToLower().Contains(filter) ||
                                                                                m.NameEn.ToLower().Contains(filter)
                                                                                ), _mapper.Map<PaginationParameters>(Pagination));
            return _mapper.Map<List<MainServiceWithAuditDto>>(entities);
        }

        public async Task<MainServiceDto> getById(string id)
        {
            var entity = await _unitOfWork.MainServiceRepository.FindOneOrDefault(m => m.Id == id && m.IsActive);
            if (entity == null)
                throw new RestfulException("Not Found main service", RestfulStatusCodes.NotFound);

            return _mapper.Map<MainServiceDto>(entity);
        }

       
    }
}
