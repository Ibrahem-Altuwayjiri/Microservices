using Services.Frontend.Web.Services.DTOs.LookupService;

namespace Services.Frontend.Web.Services.LookupService
{
    public interface ITagsService
    {
        Task<List<TagsDto>> GetTagsAsync();
        Task<TagsDto> GetTagsByIdAsync(int id);
        Task<TagsDto> CreateTagsAsync(CreateOrUpdateTagsDto dto);
        Task<TagsDto> UpdateTagsAsync(CreateOrUpdateTagsDto dto);
        Task<bool> DeactivateTag(int Id);
    }
}
