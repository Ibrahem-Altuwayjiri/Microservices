using Services.Frontend.Web.Services.DTOs.LookupService;

namespace Services.Frontend.Web.Services.LookupService
{
    public interface IDomainsService
    {
        Task<List<DomainsDto>> GetDomainsAsync();
        Task<DomainsDto> GetDomainsByIdAsync(int id);
        Task<DomainsDto> CreateDomainsAsync(CreateOrUpdateDomainsDto dto);
        Task<DomainsDto> UpdateDomainsAsync(CreateOrUpdateDomainsDto dto);
        Task<bool> DeactivateDomain(int Id);
    }
}
