using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.ServicesManagement.Application.IService.ServiceInfo;
using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Activities;
using Services.ServicesManagement.Application.Models.Dto.ServiceInfo;
using Services.ServicesManagement.Application.Models.Dto.ServiceInfo.CreateOrUpdate;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using Services.ServicesManagement.Application.Service.Abstract;
using Services.ServicesManagement.Domain.Entities.ServiceInfo;
using Services.ServicesManagement.Domain.IRepositories;
using Services.ServicesManagement.Domain.Pagination;
using Services.ServicesManagement.Infrastructure.Configuration.ExceptionHandlers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.Service.ServiceInfo
{
    public class ServiceInfoService  :  BaseService, IServiceInfoService
    {
        public ServiceInfoService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : base(unitOfWork, httpContextAccessor, mapper)
        {

        }
        #region Create Or Update Service Details
        public async Task<ServiceDetailsDto> create(CreateServiceDetailsDto dto)
        {
            var model = _mapper.Map<ServiceDetails>(dto);
            var serviceDetails = await _unitOfWork.ServiceDetailsRepository.Add(model);

            await saveServiceActivities(dto.Activities, serviceDetails.Id);
            await saveServiceTags(dto.Tags, serviceDetails.Id);
            await saveServiceDomains(dto.Domains, serviceDetails.Id);
            await saveHeaderValue(dto.HeaderValue, serviceDetails.Id);
            await saveDocumentValue(dto.DocumentValue, serviceDetails.Id);

            await _unitOfWork.CompletedAsync();

            return await getById(serviceDetails.Id);
        }

        public async Task<ServiceDetailsDto> update(UpdateServiceDetailsDto dto)
        {
            if (dto.Id == null)
                throw new RestfulException("Id is required", RestfulStatusCodes.BadRequest);

            // Load existing tracked entity from the database
            var entity = await _unitOfWork.MainServiceRepository.FindOneOrDefault(a => a.Id == dto.Id);
            if (entity == null)
                throw new RestfulException("Not Found Service", RestfulStatusCodes.NotFound);

            // Map incoming DTO onto the tracked entity to update mutable fields only
            _mapper.Map(dto, entity);

            var serviceDetails = await _unitOfWork.MainServiceRepository.Update(entity);

            await saveServiceActivities(dto.Activities, serviceDetails.Id);
            await saveServiceTags(dto.Tags, serviceDetails.Id);
            await saveServiceDomains(dto.Domains, serviceDetails.Id);
            await saveHeaderValue(dto.HeaderValue, serviceDetails.Id);
            await saveDocumentValue(dto.DocumentValue, serviceDetails.Id);

            await _unitOfWork.CompletedAsync();

            return await getById(serviceDetails.Id);
        }
        
        private async Task saveServiceTags(List<int>? serviceTags, string ServiceDetailsId)
        {
            var oldServiceTags = await _unitOfWork.ServiceTagsRepository.Find(m => m.ServiceDetailsId == ServiceDetailsId);
            foreach (var item in oldServiceTags)
            {
                await _unitOfWork.ServiceTagsRepository.Remove(item);
            }
            var newServiceTags = serviceTags?
                                   .Select(tagId => new ServiceTags
                                   {
                                       ServiceDetailsId = ServiceDetailsId,
                                       TagId = tagId
                                   })
                                   .ToList();

            if (newServiceTags != null || newServiceTags?.Count > 0)
                await _unitOfWork.ServiceTagsRepository.AddRange(newServiceTags);
        }
        private async Task saveServiceActivities(List<int>? serviceActivities, string ServiceDetailsId)
        {
            var oldServiceActivities = await _unitOfWork.ServiceActivitiesRepository.Find(m => m.ServiceDetailsId == ServiceDetailsId);
            foreach (var item in oldServiceActivities)
            {
                await _unitOfWork.ServiceActivitiesRepository.Remove(item);
            }
            var newServiceActivities = serviceActivities?
                                   .Select(ActivityId => new ServiceActivities
                                   {
                                       ServiceDetailsId = ServiceDetailsId,
                                       ActivityId = ActivityId
                                   })
                                   .ToList();

            if (newServiceActivities != null || newServiceActivities?.Count > 0)
                await _unitOfWork.ServiceActivitiesRepository.AddRange(newServiceActivities);
        }
        private async Task saveServiceDomains(List<int>? serviceDomains, string ServiceDetailsId)
        {
            var oldServiceDomains = await _unitOfWork.ServiceDomainsRepository.Find(m => m.ServiceDetailsId == ServiceDetailsId);
            foreach (var item in oldServiceDomains)
            {
                await _unitOfWork.ServiceDomainsRepository.Remove(item);
            }
            var newServiceDomains = serviceDomains?
                                   .Select(DomainId => new ServiceDomains
                                   {
                                       ServiceDetailsId = ServiceDetailsId,
                                       DomainId = DomainId
                                   })
                                   .ToList();

            if (newServiceDomains != null || newServiceDomains?.Count > 0)
                await _unitOfWork.ServiceDomainsRepository.AddRange(newServiceDomains);
        }
        private async Task saveHeaderValue(List<CreateOrUpdateHeaderValueDto>? createOrUpdateHeaderValues , string ServiceDetailsId)
        {
            var oldHeaderValue = _mapper.Map<List<HeaderValue>>(createOrUpdateHeaderValues);
            // remove old header values
            var oldHeaderValues = await _unitOfWork.HeaderValueRepository.Find(m => m.ServiceDetailsId == ServiceDetailsId);
            foreach (var item in oldHeaderValues)
            {
                await _unitOfWork.HeaderValueRepository.Remove(item.Id);
            }

            // add new header values
            var newHeaderValues = createOrUpdateHeaderValues?
                                   .Select(m => new HeaderValue
                                   {
                                       NameAr = m.NameAr,
                                       NameEn = m.NameEn,
                                       HeaderId = m.HeaderId,
                                       ServiceDetailsId = ServiceDetailsId
                                   })
                                   .ToList();
            if (newHeaderValues != null || newHeaderValues?.Count > 0)
                await _unitOfWork.HeaderValueRepository.AddRange(newHeaderValues);

        }
        private async Task saveDocumentValue(List<CreateOrUpdateDocumentValueDto>? createOrUpdateDocumentValues, string ServiceDetailsId)
        {
            // remove old document values
            var oldDocumentValues = await _unitOfWork.DocumentValueRepository.Find(m => m.ServiceDetailsId == ServiceDetailsId);
            foreach (var item in oldDocumentValues)
            {
                await _unitOfWork.DocumentValueRepository.Remove(item.Id);
            }

            // Save document values files
            await SaveFiles(createOrUpdateDocumentValues);

        }
        public async Task SaveFiles(List<CreateOrUpdateDocumentValueDto>? createOrUpdateDocumentValue)
        {
            //TODO: Implement file saving logic here
        }

        #endregion 

        public async Task<bool> activate(string id)
        {
            var entity = await _unitOfWork.ServiceDetailsRepository.FindOneOrDefault(m => m.Id == id);
            if (entity == null)
                throw new RestfulException("Not Found Service", RestfulStatusCodes.NotFound);

            if (entity.IsActive)
                throw new RestfulException("The Service is already active", RestfulStatusCodes.BadRequest);

            entity.IsActive = true;
            await _unitOfWork.ServiceDetailsRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<bool> deactivate(string id)
        {
            var entity = await _unitOfWork.ServiceDetailsRepository.FindOneOrDefault(m => m.Id == id );
            if (entity == null)
                throw new RestfulException("Not Found Service", RestfulStatusCodes.NotFound);

            if (!entity.IsActive)
                throw new RestfulException("The Service is already inactive", RestfulStatusCodes.BadRequest);

            entity.IsActive = false;
            await _unitOfWork.ServiceDetailsRepository.Update(entity);
            await _unitOfWork.CompletedAsync();

            return true;
        }

        public async Task<List<ServiceDetailsDto>> getAll(ServiceDetailsFilteringDto? filteringDto = null, PaginationParametersDto? Pagination = null)
        {
            
            return _mapper.Map<List<ServiceDetailsDto>>(await getServiceDetails(filteringDto, Pagination));
        }

        public async Task<List<ServiceDetailsWithAuditDto>> getAllWithAudit(ServiceDetailsFilteringDto? filteringDto = null, PaginationParametersDto? Pagination = null)
        {
            return _mapper.Map<List<ServiceDetailsWithAuditDto>>(getServiceDetails(filteringDto, Pagination, false));
        }

        private async Task<IEnumerable<ServiceDetails>?> getServiceDetails(ServiceDetailsFilteringDto? filteringDto = null, PaginationParametersDto? Pagination = null, bool CheckIsActive = true)
        {
            if (filteringDto != null && filteringDto?.filter != null)
                filteringDto.filter = filteringDto.filter.ToLower();

            var entities = await _unitOfWork.ServiceDetailsRepository.FindWithInclude(m =>
                                                                                (CheckIsActive ? m.IsActive : true) && (
                                                                                filteringDto == null ||
                                                                                string.IsNullOrEmpty(filteringDto.filter) ||
                                                                                m.NameAr.ToLower().Contains(filteringDto.filter) ||
                                                                                m.NameEn.ToLower().Contains(filteringDto.filter) ||
                                                                                m.DescriptionAr.ToLower().Contains(filteringDto.filter) ||
                                                                                m.DescriptionEn.ToLower().Contains(filteringDto.filter) ||
                                                                                m.MainServiceId == filteringDto.MainServiceId ||
                                                                                m.SubServiceId == filteringDto.SubServiceId ||
                                                                                m.SubSubServiceId == filteringDto.SubSubServiceId ||
                                                                                (filteringDto.ActivityIds != null && m.ServiceActivities.Any(s => filteringDto.ActivityIds.Any(n => n.Equals(s.ActivityId)))) ||
                                                                                (filteringDto.DomainIds != null && m.ServiceDomains.Any(s => filteringDto.DomainIds.Any(n => n.Equals(s.DomainId)))) ||
                                                                                (filteringDto.TagIds != null && m.ServiceTags.Any(s => filteringDto.TagIds.Any(n => n.Equals(s.TagId))))
                                                                                ), _mapper.Map<PaginationParameters>(Pagination),
                                                                                include: query => query
                                                                                    .Include(o => o.ServiceTags).ThenInclude(t => t.Tags)
                                                                                    .Include(o => o.ServiceDomains).ThenInclude(d => d.Domains)
                                                                                    .Include(o => o.ServiceActivities).ThenInclude(a => a.Activities)
                                                                                    .Include(o => o.MainService)
                                                                                    .Include(o => o.SubService)
                                                                                    .Include(o => o.SubSubService)
                                                                                    .Include(o => o.HeaderValue).ThenInclude(h => h.Header)
                                                                                    .Include(o => o.DocumentValue).ThenInclude(d => d.DocumentName)

                                                                                );

            //TODO: fix the looping include the ServiceDetails in ServiceActivities , ServiceTags , ServiceDomains, HeaderValue , DocumentValue
            return entities;
        }

        public async Task<ServiceDetailsDto> getById(string id)
        {
            var entity = await _unitOfWork.ServiceDetailsRepository.FindOneOrDefault(m => m.Id == id && m.IsActive);
            if (entity == null)
                throw new RestfulException("Not Found Service", RestfulStatusCodes.NotFound);

            return _mapper.Map<ServiceDetailsDto>(entity);
        }

        
    }
}
