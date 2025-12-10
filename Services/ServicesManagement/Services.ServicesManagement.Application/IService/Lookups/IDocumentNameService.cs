using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.DocumentName;
using Services.ServicesManagement.Domain.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.IService.Lookups
{
    public interface IDocumentNameService
    {
        Task<DocumentNameDto> create(CreateOrUpdateDocumentNameDto dto);
        Task<DocumentNameDto> update(CreateOrUpdateDocumentNameDto dto);
        Task<bool> activate(int id);
        Task<bool> deactivate(int id);
        Task<DocumentNameDto> getById(int id);
        Task<List<DocumentNameDto>> getAll(PaginationParametersDto? Pagination = null);
        Task<List<DocumentNameWithAuditDto>> getAllWithAudit(PaginationParametersDto? Pagination = null);
    }
}
