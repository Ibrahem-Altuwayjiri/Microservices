using AutoMapper;
using Microsoft.AspNetCore.Http;
using Services.ServicesManagement.Application.IService.Lookups;
using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Activities;
using Services.ServicesManagement.Application.Models.Dto.DocumentName;
using Services.ServicesManagement.Application.Service.Abstract;
using Services.ServicesManagement.Domain.Entities.Lookups;
using Services.ServicesManagement.Domain.IRepositories;
using Services.ServicesManagement.Domain.Pagination;
using Services.ServicesManagement.Infrastructure.Configuration.ExceptionHandlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.Service.Lookups
{
    public class DocumentNameService : BaseService, IDocumentNameService
    {
        public DocumentNameService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor, mapper)
        {
        }

        public async Task<DocumentNameDto> create(CreateOrUpdateDocumentNameDto dto)
        {
            var model = _mapper.Map<DocumentName>(dto);
            var result = await _unitOfWork.DocumentNameRepository.Add(model);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<DocumentNameDto>(result);
        }
        public async Task<DocumentNameDto> update(CreateOrUpdateDocumentNameDto dto)
        {
            if (dto.Id <= 0)
                throw new RestfulException("Id is required", RestfulStatusCodes.BadRequest);

            // Load existing tracked entity from the database
            var entity = await _unitOfWork.DocumentNameRepository.FindOneOrDefault(a => a.Id == dto.Id);
            if (entity == null)
                throw new RestfulException("Not Found document name", RestfulStatusCodes.NotFound);

            // Map incoming DTO onto the tracked entity to update mutable fields only
            _mapper.Map(dto, entity);

            var result = await _unitOfWork.DocumentNameRepository.Update(entity);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<DocumentNameDto>(result);
        }

        public async Task<bool> activate(int id)
        {
            var entity = await _unitOfWork.DocumentNameRepository.FindOneOrDefault(m => m.Id == id );
            if (entity == null)
                throw new RestfulException("Not Found document name", RestfulStatusCodes.NotFound);

            if (entity.IsActive)
                throw new RestfulException("The document name is already active", RestfulStatusCodes.BadRequest);

            entity.IsActive = true;
            await _unitOfWork.DocumentNameRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<bool> deactivate(int id)
        {
            var entity = await _unitOfWork.DocumentNameRepository.FindOneOrDefault(m => m.Id == id);
            if (entity == null)
                throw new RestfulException("Not Found document name", RestfulStatusCodes.NotFound);

            if (!entity.IsActive)
                throw new RestfulException("The document name is already inactive", RestfulStatusCodes.BadRequest);

            entity.IsActive = false;
            await _unitOfWork.DocumentNameRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<List<DocumentNameDto>> getAll(PaginationParametersDto? Pagination = null)
        {
            var entities = await _unitOfWork.DocumentNameRepository.Find(m => m.IsActive, _mapper.Map<PaginationParameters>(Pagination));
            return _mapper.Map<List<DocumentNameDto>>(entities);
        }

        public async Task<List<DocumentNameWithAuditDto>> getAllWithAudit(PaginationParametersDto? Pagination = null)
        {
            var entities = await _unitOfWork.DocumentNameRepository.Find(m => true, _mapper.Map<PaginationParameters>(Pagination));
            return _mapper.Map<List<DocumentNameWithAuditDto>>(entities);
        }

        public async Task<DocumentNameDto> getById(int id)
        {
            var entity = await _unitOfWork.DocumentNameRepository.FindOneOrDefault(m => m.Id == id && m.IsActive);
            if (entity == null)
                throw new RestfulException("Not Found document name", RestfulStatusCodes.NotFound);

            return _mapper.Map<DocumentNameDto>(entity);
        }

      
    }
}
