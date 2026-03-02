using Services.Frontend.Web.Services.DTOs.LookupService;

namespace Services.Frontend.Web.Services.LookupService
{
    public interface IActivitiesService
    {
        Task<List<ActivitiesDto>> GetActivitiesAsync();
        Task<ActivitiesDto> GetActivitiesByIdAsync(int id);
        Task<ActivitiesDto> CreateActivitiesAsync(CreateOrUpdateActivitiesDto dto);
        Task<ActivitiesDto> UpdateActivitiesAsync(CreateOrUpdateActivitiesDto dto);
        Task<bool> DeactivateActivity(int Id);
    }
}
