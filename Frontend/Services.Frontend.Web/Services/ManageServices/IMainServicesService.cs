using Services.Frontend.Web.Services.DTOs.ManageServices;

namespace Services.Frontend.Web.Services.ManageServices
{
    public interface IMainServicesService
    {
        Task<List<MainServiceDto>> GetMainServicesAsync(string filter = "");
        Task<MainServiceDto> GetMainServiceByIdAsync(string id);
        Task<MainServiceDto> CreateMainServiceAsync(CreateOrUpdateMainServiceDto dto);
        Task<MainServiceDto> UpdateMainServiceAsync(CreateOrUpdateMainServiceDto dto);
        Task<bool> DeactivateMainService(string Id);
    }
}
