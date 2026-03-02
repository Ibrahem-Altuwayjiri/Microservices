using Services.Frontend.Web.Services.DTOs.ManageServices;

namespace Services.Frontend.Web.Services.ManageServices
{
    public interface IServiceDetailsService
    {
        Task<List<ServiceDetailsDto>> GetAllServiceDetailsAsync();
        Task<ServiceDetailsDto> GetServiceDetailsByIdAsync(string id);
        Task<ServiceDetailsDto> CreateServiceDetailsAsync(CreateOrUpdateServiceDetailsDto dto);
        Task<ServiceDetailsDto> UpdateServiceDetailsAsync(CreateOrUpdateServiceDetailsDto dto);
    }
}
