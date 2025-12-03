using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Activities;
using Services.ServicesManagement.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.IService.Lookups
{
    public interface IActivitiesService
    {
        Task<ActivitiesDto> create(CreateOrUpdateActivitiesDto activitiesDto);
        Task<ActivitiesDto> update(CreateOrUpdateActivitiesDto activitiesDto);
        Task<bool> activate(int id);
        Task<bool> deactivate(int id);
        Task<ActivitiesDto> getById(int id);
        Task<List<ActivitiesDto>> getAll(PaginationParametersDto? Pagination = null);
        Task<List<ActivitiesWithAuditDto>> getAllWithAudit(PaginationParametersDto? Pagination = null);
    }
}
