using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Domains;
using Services.ServicesManagement.Domain.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.IService.Lookups
{
    public interface IDomainsService
    {
        Task<DomainsDto> create(CreateOrUpdateDomainsDto dto);
        Task<DomainsDto> update(CreateOrUpdateDomainsDto dto);
        Task<bool> activate(int id);
        Task<bool> deactivate(int id);
        Task<DomainsDto> getById(int id);
        Task<List<DomainsDto>> getAll(PaginationParametersDto? Pagination = null);
        Task<List<DomainsWithAuditDto>> getAllWithAudit(PaginationParametersDto? Pagination = null);
    }
}
