using Services.Frontend.Web.Services.DTOs.LookupService;

namespace Services.Frontend.Web.Services.DTOs.ManageServices
{
    public class ServicesCatalogViewModel
    {
        public List<ServiceDetailsDto> Services { get; set; } = [];
        public List<DomainsDto> AllDomains { get; set; } = [];
        public List<ActivitiesDto> AllActivities { get; set; } = [];
        public List<TagsDto> AllTags { get; set; } = [];

        // Current filter values
        public string? SearchTerm { get; set; }
        public int? DomainId { get; set; }
        public List<int> ActivityIds { get; set; } = [];
        public List<int> TagIds { get; set; } = [];

        // Pagination
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
