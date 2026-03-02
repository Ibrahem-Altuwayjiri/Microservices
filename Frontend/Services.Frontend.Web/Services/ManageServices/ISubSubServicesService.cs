using Services.Frontend.Web.Services.DTOs.ManageServices;

namespace Services.Frontend.Web.Services.ManageServices
{
    public interface ISubSubServicesService
    {
        Task<List<SubSubServiceDto>> GetSubSubServicesAsync(string filter = "");
        Task<SubSubServiceDto> GetSubSubServiceByIdAsync(string id);
        Task<SubSubServiceDto> CreateSubSubServiceAsync(CreateOrUpdateSubSubServiceDto dto);
        Task<SubSubServiceDto> UpdateSubSubServiceAsync(CreateOrUpdateSubSubServiceDto dto);
        Task<bool> DeactivateSubSubService(string Id);
    }
}
