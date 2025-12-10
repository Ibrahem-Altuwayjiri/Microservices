using AutoMapper;
using Microsoft.AspNetCore.Http;
using Services.ServicesManagement.Application.IService.Lookups;
using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Header;
using Services.ServicesManagement.Application.Service.Abstract;
using Services.ServicesManagement.Domain.Entities.Lookups;
using Services.ServicesManagement.Domain.IRepositories;
using Services.ServicesManagement.Domain.Pagination;
using Services.ServicesManagement.Infrastructure.Configuration.ExceptionHandlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.Service.Lookups
{
    public class HeaderService : BaseService, IHeaderService
    {
        public HeaderService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor, mapper)
        {
        }

        public async Task<HeaderDto> create(CreateOrUpdateHeaderDto dto)
        {
            var model = _mapper.Map<Header>(dto);
            var result = await _unitOfWork.HeaderRepository.Add(model);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<HeaderDto>(result);
        }
        public async Task<HeaderDto> update(CreateOrUpdateHeaderDto dto)
        {
            if (dto.Id <= 0)
                throw new RestfulException("Id is required", RestfulStatusCodes.BadRequest);

            // Load existing tracked entity from the database
            var entity = await _unitOfWork.HeaderRepository.FindOneOrDefault(a => a.Id == dto.Id);
            if (entity == null)
                throw new RestfulException("Not Found header", RestfulStatusCodes.NotFound);

            // Map incoming DTO onto the tracked entity to update mutable fields only
            _mapper.Map(dto, entity);

            var result = await _unitOfWork.HeaderRepository.Update(entity);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<HeaderDto>(result);
        }

        public async Task<bool> activate(int id)
        {
            var entity = await _unitOfWork.HeaderRepository.FindOneOrDefault(m => m.Id == id );
            if (entity == null)
                throw new RestfulException("Not Found header", RestfulStatusCodes.NotFound);

            if (entity.IsActive)
                throw new RestfulException("The header is already active", RestfulStatusCodes.BadRequest);

            entity.IsActive = true;
            await _unitOfWork.HeaderRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<bool> deactivate(int id)
        {
            var entity = await _unitOfWork.HeaderRepository.FindOneOrDefault(m => m.Id == id );
            if (entity == null)
                throw new RestfulException("Not Found header", RestfulStatusCodes.NotFound);
            if (!entity.IsActive)
                throw new RestfulException("The header is already inactive", RestfulStatusCodes.BadRequest);

            entity.IsActive = false;
            await _unitOfWork.HeaderRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<List<HeaderDto>> getAll(PaginationParametersDto? Pagination = null)
        {
            var entities = await _unitOfWork.HeaderRepository.Find(m => m.IsActive, _mapper.Map<PaginationParameters>(Pagination));
            return _mapper.Map<List<HeaderDto>>(entities);
        }

        public async Task<List<HeaderWithAuditDto>> getAllWithAudit(PaginationParametersDto? Pagination = null)
        {
            var entities = await _unitOfWork.HeaderRepository.Find(m => true, _mapper.Map<PaginationParameters>(Pagination));
            return _mapper.Map<List<HeaderWithAuditDto>>(entities);
        }

        public async Task<HeaderDto> getById(int id)
        {
            var entity = await _unitOfWork.HeaderRepository.FindOneOrDefault(m => m.Id == id && m.IsActive);
            if (entity == null)
                throw new RestfulException("Not Found header", RestfulStatusCodes.NotFound);

            return _mapper.Map<HeaderDto>(entity);
        }

       
    }
}
