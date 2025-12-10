using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using Services.ServicesManagement.Domain.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.IService.ServiceStructure
{
    public interface ISubSubServiceService
    {
        Task<SubSubServiceDto> create(CreateSubSubServiceDto dto);
        Task<SubSubServiceDto> update(UpdateSubSubServiceDto dto);
        Task<bool> activate(string id);
        Task<bool> deactivate(string id);
        Task<SubSubServiceDto> getById(string id);
        Task<List<SubSubServiceDto>> getAll(string filter = "", PaginationParametersDto? Pagination = null);
        Task<List<SubSubServiceWithAuditDto>> getAllWithAudit(string filter = "", PaginationParametersDto? Pagination = null);
    }
}
