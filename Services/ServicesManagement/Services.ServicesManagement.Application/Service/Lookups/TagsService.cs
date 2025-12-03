using AutoMapper;
using Microsoft.AspNetCore.Http;
using Services.ServicesManagement.Application.IService.Lookups;
using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Tags;
using Services.ServicesManagement.Application.Service.Abstract;
using Services.ServicesManagement.Domain.Entities.Lookups;
using Services.ServicesManagement.Domain.IRepositories;
using Services.ServicesManagement.Domain.Pagination;
using Services.ServicesManagement.Infrastructure.Configuration.ExceptionHandlers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.Service.Lookups
{
    public class TagsService : BaseService, ITagsService
    {
        public TagsService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor, mapper)
        {
        }

        public async Task<TagsDto> create(CreateOrUpdateTagsDto dto)
        {
            var model = _mapper.Map<Tags>(dto);
            var result = await _unitOfWork.TagsRepository.Add(model);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<TagsDto>(result);
        }

        public async Task<TagsDto> update(CreateOrUpdateTagsDto dto)
        {
            if (dto.Id <= 0)
                throw new RestfulException("Id is required", RestfulStatusCodes.BadRequest);

            // Load existing tracked entity from the database
            var entity = await _unitOfWork.TagsRepository.FindOneOrDefault(a => a.Id == dto.Id);
            if (entity == null)
                throw new RestfulException("Not Found tag", RestfulStatusCodes.NotFound);

            // Map incoming DTO onto the tracked entity to update mutable fields only
            _mapper.Map(dto, entity);

            var result = await _unitOfWork.TagsRepository.Update(entity);
            await _unitOfWork.CompletedAsync();
            return _mapper.Map<TagsDto>(result);
        }

        public async Task<bool> activate(int id)
        {
            var entity = await _unitOfWork.TagsRepository.FindOneOrDefault(m => m.Id == id);
            if (entity == null)
                throw new RestfulException("Not Found tag", RestfulStatusCodes.NotFound);

            if (entity.IsActive)
                throw new RestfulException("The tag is already active", RestfulStatusCodes.BadRequest);

            entity.IsActive = true;
            await _unitOfWork.TagsRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<bool> deactivate(int id)
        {
            var entity = await _unitOfWork.TagsRepository.FindOneOrDefault(m => m.Id == id);
            if (entity == null)
                throw new RestfulException("Not Found tag", RestfulStatusCodes.NotFound);

            if (!entity.IsActive)
                throw new RestfulException("The tag is already inactive", RestfulStatusCodes.BadRequest);

            entity.IsActive = false;
            await _unitOfWork.TagsRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<List<TagsDto>> getAll(PaginationParametersDto? Pagination = null)
        {
            var entities = await _unitOfWork.TagsRepository.Find(m => m.IsActive, _mapper.Map<PaginationParameters>(Pagination));
            return _mapper.Map<List<TagsDto>>(entities);
        }

        public async Task<List<TagsWithAuditDto>> getAllWithAudit(PaginationParametersDto? Pagination = null)
        {
            var entities = await _unitOfWork.TagsRepository.Find(m => true, _mapper.Map<PaginationParameters>(Pagination));
            return _mapper.Map<List<TagsWithAuditDto>>(entities);
        }

        public async Task<TagsDto> getById(int id)
        {
            var entity = await _unitOfWork.TagsRepository.FindOneOrDefault(m => m.Id == id && m.IsActive);
            if (entity == null)
                throw new RestfulException("Not Found tag", RestfulStatusCodes.NotFound);

            return _mapper.Map<TagsDto>(entity);
        }

       
    }
}
