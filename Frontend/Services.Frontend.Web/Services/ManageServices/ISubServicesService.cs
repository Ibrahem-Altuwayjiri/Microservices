using Services.Frontend.Web.Services.DTOs.ManageServices;

namespace Services.Frontend.Web.Services.ManageServices
{
    public interface ISubServicesService
    {
        Task<List<SubServiceDto>> GetSubServicesAsync();
        Task<SubServiceDto> GetSubServiceByIdAsync(string id);
        Task<SubServiceDto> CreateSubServiceAsync(CreateOrUpdateSubServiceDto dto);
        Task<SubServiceDto> UpdateSubServiceAsync(CreateOrUpdateSubServiceDto dto);
        Task<bool> DeactivateSubService(string Id);
    }
}
