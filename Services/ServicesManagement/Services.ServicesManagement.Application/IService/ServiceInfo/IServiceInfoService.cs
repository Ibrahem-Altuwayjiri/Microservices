using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Activities;
using Services.ServicesManagement.Application.Models.Dto.ServiceInfo;
using Services.ServicesManagement.Application.Models.Dto.ServiceInfo.CreateOrUpdate;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.IService.ServiceInfo
{
    public interface IServiceInfoService
    {
        Task<ServiceDetailsDto> create(CreateServiceDetailsDto serviceDetails);
        Task<ServiceDetailsDto> update(UpdateServiceDetailsDto serviceDetails);
        Task<bool> activate(string id);
        Task<bool> deactivate(string id);
        Task<ServiceDetailsDto> getById(string id);
        Task<List<ServiceDetailsDto>> getAll(ServiceDetailsFilteringDto? filteringDto = null, PaginationParametersDto? Pagination = null);
        Task<List<ServiceDetailsWithAuditDto>> getAllWithAudit(ServiceDetailsFilteringDto? filteringDto = null, PaginationParametersDto? Pagination = null);
    }
}
