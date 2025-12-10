using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Tags;
using Services.ServicesManagement.Domain.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.IService.Lookups
{
    public interface ITagsService
    {
        Task<TagsDto> create(CreateOrUpdateTagsDto dto);
        Task<TagsDto> update(CreateOrUpdateTagsDto dto);
        Task<bool> activate(int id);
        Task<bool> deactivate(int id);
        Task<TagsDto> getById(int id);
        Task<List<TagsDto>> getAll(PaginationParametersDto? Pagination = null);
        Task<List<TagsWithAuditDto>> getAllWithAudit(PaginationParametersDto? Pagination = null);
    }
}
