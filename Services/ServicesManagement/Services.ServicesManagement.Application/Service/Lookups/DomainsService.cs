using AutoMapper;
using Microsoft.AspNetCore.Http;
using Services.ServicesManagement.Application.IService.Lookups;
using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Activities;
using Services.ServicesManagement.Application.Models.Dto.Domains;
using Services.ServicesManagement.Application.Service.Abstract;
using Services.ServicesManagement.Domain.Entities.Lookups;
using Services.ServicesManagement.Domain.IRepositories;
using Services.ServicesManagement.Domain.Pagination;
using Services.ServicesManagement.Infrastructure.Configuration.ExceptionHandlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.Service.Lookups
{
    public class DomainsService : BaseService, IDomainsService
    {
        public DomainsService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor, mapper)
        {
        }

        public async Task<DomainsDto> create(CreateOrUpdateDomainsDto dto)
        {
            var model = _mapper.Map<Domains>(dto);
            var result = await _unitOfWork.DomainsRepository.Add(model);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<DomainsDto>(result);
        }
        public async Task<DomainsDto> update(CreateOrUpdateDomainsDto dto)
        {
            if (dto.Id <= 0)
                throw new RestfulException("Id is required", RestfulStatusCodes.BadRequest);

            // Load existing tracked entity from the database
            var entity = await _unitOfWork.DomainsRepository.FindOneOrDefault(a => a.Id == dto.Id);
            if (entity == null)
                throw new RestfulException("Not Found domain", RestfulStatusCodes.NotFound);

            // Map incoming DTO onto the tracked entity to update mutable fields only
            _mapper.Map(dto, entity);

            var result = await _unitOfWork.DomainsRepository.Update(entity);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<DomainsDto>(result);
        }

        public async Task<bool> activate(int id)
        {
            var entity = await _unitOfWork.DomainsRepository.FindOneOrDefault(m => m.Id == id );
            if (entity == null)
                throw new RestfulException("Not Found domain", RestfulStatusCodes.NotFound);

            if (entity.IsActive)
                throw new RestfulException("The domain is already active", RestfulStatusCodes.BadRequest);

            entity.IsActive = true;
            await _unitOfWork.DomainsRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<bool> deactivate(int id)
        {
            var entity = await _unitOfWork.DomainsRepository.FindOneOrDefault(m => m.Id == id );
            if (entity == null)
                throw new RestfulException("Not Found domain", RestfulStatusCodes.NotFound);
            if (!entity.IsActive)
                throw new RestfulException("The domain is already inactive", RestfulStatusCodes.BadRequest);

            entity.IsActive = false;
            await _unitOfWork.DomainsRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<List<DomainsDto>> getAll(PaginationParametersDto? Pagination = null)
        {
            var entities = await _unitOfWork.DomainsRepository.Find(m => m.IsActive, _mapper.Map<PaginationParameters>(Pagination));
            return _mapper.Map<List<DomainsDto>>(entities);
        }

        public async Task<List<DomainsWithAuditDto>> getAllWithAudit(PaginationParametersDto? Pagination = null)
        {
            var entities = await _unitOfWork.DomainsRepository.Find(m => true, _mapper.Map<PaginationParameters>(Pagination));
            return _mapper.Map<List<DomainsWithAuditDto>>(entities);
        }

        public async Task<DomainsDto> getById(int id)
        {
            var entity = await _unitOfWork.DomainsRepository.FindOneOrDefault(m => m.Id == id && m.IsActive);
            if (entity == null)
                throw new RestfulException("Not Found domain", RestfulStatusCodes.NotFound);

            return _mapper.Map<DomainsDto>(entity);
        }

       
    }
}
