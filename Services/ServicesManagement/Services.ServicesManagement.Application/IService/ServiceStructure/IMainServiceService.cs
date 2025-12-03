using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure.CreateOrUpdate;
using Services.ServicesManagement.Domain.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.IService.ServiceStructure
{
    public interface IMainServiceService
    {
        Task<MainServiceDto> create(CreateMainServiceDto dto);
        Task<MainServiceDto> update(UpdateMainServiceDto dto);
        Task<bool> activate(string id);
        Task<bool> deactivate(string id);
        Task<MainServiceDto> getById(string id);
        Task<List<MainServiceDto>> getAll(string filter = "", PaginationParametersDto? Pagination = null);
        Task<List<MainServiceWithAuditDto>> getAllWithAudit(string filter = "", PaginationParametersDto? Pagination = null);
    }
}
