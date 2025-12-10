using AutoMapper;
using Microsoft.AspNetCore.Http;
using Services.ServicesManagement.Application.IService.Lookups;
using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Activities;
using Services.ServicesManagement.Application.Service.Abstract;
using Services.ServicesManagement.Domain.Entities.Lookups;
using Services.ServicesManagement.Domain.IRepositories;
using Services.ServicesManagement.Domain.Pagination;
using Services.ServicesManagement.Infrastructure.Configuration.ExceptionHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.Service.Lookups
{
    public class ActivitiesService :  BaseService, IActivitiesService
    {

        public ActivitiesService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, httpContextAccessor, mapper)
        {

        }
        public async Task<ActivitiesDto> create(CreateOrUpdateActivitiesDto activitiesDto)
        {
            var model = _mapper.Map<Activities>(activitiesDto);
            var result = await _unitOfWork.ActivitiesRepository.Add(model);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<ActivitiesDto>(result);
        }
        public async Task<ActivitiesDto> update(CreateOrUpdateActivitiesDto activitiesDto)
        {
            if (activitiesDto.Id <= 0)
                throw new RestfulException("Id is required", RestfulStatusCodes.BadRequest);

            //TODO: todo the same way for update in all services

            // Load existing tracked entity from the database
            var entity = await _unitOfWork.ActivitiesRepository.FindOneOrDefault(a => a.Id == activitiesDto.Id);
            if (entity == null)
                throw new RestfulException("Not Found activity", RestfulStatusCodes.NotFound);

            // Map incoming DTO onto the tracked entity to update mutable fields only
            _mapper.Map(activitiesDto, entity);

            // Persist changes via UnitOfWork so AutoHistory uses correct OriginalValues
            var result = await _unitOfWork.ActivitiesRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return _mapper.Map<ActivitiesDto>(result);
        }

        public async Task<bool> activate(int id)
        {
            var entity = await  _unitOfWork.ActivitiesRepository.FindOneOrDefault(m => m.Id == id );
            if (entity == null)
                throw new RestfulException("Not Found activity", RestfulStatusCodes.NotFound);

            if (entity.IsActive)
                throw new RestfulException("The activity is already active", RestfulStatusCodes.BadRequest);

            entity.IsActive = true;
            await _unitOfWork.ActivitiesRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<bool> deactivate(int id)
        {
            var entity = await _unitOfWork.ActivitiesRepository.FindOneOrDefault(m => m.Id == id);
            if (entity == null)
                throw new RestfulException("Not Found activity", RestfulStatusCodes.NotFound);

            if (!entity.IsActive)
                throw new RestfulException("The activity is already inactive", RestfulStatusCodes.BadRequest);

            entity.IsActive = false;
            await _unitOfWork.ActivitiesRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<List<ActivitiesDto>> getAll(PaginationParametersDto? Pagination = null)
        {
            var entities = await _unitOfWork.ActivitiesRepository.Find(m => m.IsActive, _mapper.Map<PaginationParameters>(Pagination));
            return _mapper.Map<List<ActivitiesDto>>(entities);
        }

        public async Task<List<ActivitiesWithAuditDto>> getAllWithAudit(PaginationParametersDto? Pagination = null)
        {
            var entities = await _unitOfWork.ActivitiesRepository.Find(m => true, _mapper.Map<PaginationParameters>(Pagination));
            return _mapper.Map<List<ActivitiesWithAuditDto>>(entities);
        }

        public async Task<ActivitiesDto> getById(int id)
        {
            var entity = await  _unitOfWork.ActivitiesRepository.FindOneOrDefault(m => m.Id == id && m.IsActive);
            if (entity == null)
                throw new RestfulException("Not Found activity", RestfulStatusCodes.NotFound);
            return _mapper.Map<ActivitiesDto>(entity);
        }

        
    }
}
