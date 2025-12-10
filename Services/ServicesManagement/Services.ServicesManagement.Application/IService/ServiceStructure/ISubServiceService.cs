using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using Services.ServicesManagement.Domain.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.IService.ServiceStructure
{
    public interface ISubServiceService
    {
        Task<SubServiceDto> create(CreateSubServiceDto dto);
        Task<SubServiceDto> update(UpdateSubServiceDto dto);
        Task<bool> activate(string id);
        Task<bool> deactivate(string id);
        Task<SubServiceDto> getById(string id);
        Task<List<SubServiceDto>> getAll(string filter = "", PaginationParametersDto? Pagination = null);
        Task<List<SubServiceWithAuditDto>> getAllWithAudit(string filter = "", PaginationParametersDto? Pagination = null);
    }
}
