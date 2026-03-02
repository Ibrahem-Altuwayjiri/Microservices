using Services.Frontend.Web.Services.DTOs.LookupService;

namespace Services.Frontend.Web.Services.LookupService
{
    public interface IHeadersService
    {
        Task<List<HeaderDto>> GetHeadersAsync();
        Task<HeaderDto> GetHeadersByIdAsync(int id);
        Task<HeaderDto> CreateHeadersAsync(CreateOrUpdateHeaderDto dto);
        Task<HeaderDto> UpdateHeadersAsync(CreateOrUpdateHeaderDto dto);
        Task<bool> DeactivateHeader(int Id);
    }
}
