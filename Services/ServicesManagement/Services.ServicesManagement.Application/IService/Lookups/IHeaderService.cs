using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Header;
using Services.ServicesManagement.Domain.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.IService.Lookups
{
    public interface IHeaderService
    {
        Task<HeaderDto> create(CreateOrUpdateHeaderDto dto);
        Task<HeaderDto> update(CreateOrUpdateHeaderDto dto);
        Task<bool> activate(int id);
        Task<bool> deactivate(int id);
        Task<HeaderDto> getById(int id);
        Task<List<HeaderDto>> getAll(PaginationParametersDto? Pagination = null);
        Task<List<HeaderWithAuditDto>> getAllWithAudit(PaginationParametersDto? Pagination = null);
    }
}
