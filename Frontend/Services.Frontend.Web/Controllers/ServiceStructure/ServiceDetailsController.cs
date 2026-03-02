using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Frontend.Web.Services.ManageServices;
using Services.Frontend.Web.Services.LookupService;
using Services.Frontend.Web.Services.DTOs.ManageServices;
using Services.Frontend.Web.Models.Dto;

namespace Services.Frontend.Web.Controllers
{
    [Authorize]
    public class ServiceDetailsController : Controller
    {
        private readonly IServiceDetailsService _serviceDetails;
        private readonly IActivitiesService _activitiesService;
        private readonly IDomainsService _domainsService;
        private readonly ITagsService _tagsService;
        private readonly IHeadersService _headersService;
        private readonly IMainServicesService _mainServicesService;
        private readonly ISubServicesService _subServicesService;
        private readonly ISubSubServicesService _subSubServicesService;
        private readonly ILogger<ServiceDetailsController> _logger;

        public ServiceDetailsController(
            IServiceDetailsService serviceDetails,
            IActivitiesService activitiesService,
            IDomainsService domainsService,
            ITagsService tagsService,
            IHeadersService headersService,
            IMainServicesService mainServicesService,
            ISubServicesService subServicesService,
            ISubSubServicesService subSubServicesService,
            ILogger<ServiceDetailsController> logger)
        {
            _serviceDetails = serviceDetails;
            _activitiesService = activitiesService;
            _domainsService = domainsService;
            _tagsService = tagsService;
            _headersService = headersService;
            _mainServicesService = mainServicesService;
            _subServicesService = subServicesService;
            _subSubServicesService = subSubServicesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
            var services = await _serviceDetails.GetAllServiceDetailsAsync();
                return PartialView("List", services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching ServiceDetails");
                return PartialView("List", new List<ServiceDetailsDto>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListAsCards()
        {
            try
            {
                var services = await _serviceDetails.GetAllServiceDetailsAsync();
                return PartialView("ListAsCards", services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching ServiceDetails");
                return PartialView("ListAsCards", new List<ServiceDetailsDto>());
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ServicesCatalog(
            string? search,
            int? domainId,
            [FromQuery] List<int> activityIds,
            [FromQuery] List<int> tagIds,
            int page = 1,
            int pageSize = 9)
        {
            try
            {
                var allServices = await _serviceDetails.GetAllServiceDetailsAsync();
                var allDomains = await _domainsService.GetDomainsAsync();
                var allActivities = await _activitiesService.GetActivitiesAsync();
                var allTags = await _tagsService.GetTagsAsync();

                var filtered = allServices.Where(s => s.IsActive).AsEnumerable();

                // Search by name
                if (!string.IsNullOrWhiteSpace(search))
                {
                    var term = search.Trim();
                    filtered = filtered.Where(s =>
                        (s.NameAr != null && s.NameAr.Contains(term, StringComparison.OrdinalIgnoreCase)) ||
                        (s.NameEn != null && s.NameEn.Contains(term, StringComparison.OrdinalIgnoreCase)));
                }

                // Filter by single domain
                if (domainId.HasValue && domainId.Value > 0)
                {
                    filtered = filtered.Where(s =>
                        s.Domains != null && s.Domains.Any(d => d.Id == domainId.Value));
                }

                // Filter by multiple activities
                if (activityIds != null && activityIds.Count > 0)
                {
                    filtered = filtered.Where(s =>
                        s.Activities != null && activityIds.All(aid => s.Activities.Any(a => a.Id == aid)));
                }

                // Filter by multiple tags
                if (tagIds != null && tagIds.Count > 0)
                {
                    filtered = filtered.Where(s =>
                        s.Tags != null && tagIds.All(tid => s.Tags.Any(t => t.Id == tid)));
                }

                var totalCount = filtered.Count();
                var pagedServices = filtered
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var viewModel = new ServicesCatalogViewModel
                {
                    Services = pagedServices,
                    AllDomains = allDomains ?? [],
                    AllActivities = allActivities ?? [],
                    AllTags = allTags ?? [],
                    SearchTerm = search,
                    DomainId = domainId,
                    ActivityIds = activityIds ?? [],
                    TagIds = tagIds ?? [],
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalCount = totalCount
                };

                ViewData["Title"] = "Services Catalog";
                ViewData["BreadcrumbItems"] = new List<(string, string)>
                {
                    ("Services", "")
                };

                return View("ServicesCatalog", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading services catalog");
                return View("ServicesCatalog", new ServicesCatalogViewModel());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Form(string? id)
        {
            try
            {
                var dto = new CreateOrUpdateServiceDetailsDto();

                if (!string.IsNullOrEmpty(id))
                {
                    var service = await _serviceDetails.GetServiceDetailsByIdAsync(id);
                    if (service != null)
                    {
                        dto = CreateOrUpdateServiceDetailsDto.FromServiceDetailsDto(service);
                    }
                }

                // Load all available lookups
                dto.AllActivities = await _activitiesService.GetActivitiesAsync();
                dto.AllDomains = await _domainsService.GetDomainsAsync();
                dto.AllTags = await _tagsService.GetTagsAsync();
                dto.AllHeaders = await _headersService.GetHeadersAsync();

                // Load service structure lists for dropdowns
                dto.AllMainServices = await _mainServicesService.GetMainServicesAsync();
                dto.AllSubServices = await _subServicesService.GetSubServicesAsync();
                dto.AllSubSubServices = await _subSubServicesService.GetSubSubServicesAsync();

                return PartialView("Form", dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading ServiceDetails form");
                return PartialView("Form", new CreateOrUpdateServiceDetailsDto());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateServiceDetailsDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("ServiceDetails creation validation failed");
                    return BadRequest(new
                    {
                        isSuccess = false,
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                _logger.LogInformation("Creating ServiceDetails");
            var result = await _serviceDetails.CreateServiceDetailsAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "ServiceDetails created successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ServiceDetails");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdateServiceDetailsDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("ServiceDetails update validation failed");
                    return BadRequest(new
                    {
                        isSuccess = false,
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                _logger.LogInformation("Updating ServiceDetails: {Id}", dto.Id);
            var result = await _serviceDetails.UpdateServiceDetailsAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "ServiceDetails updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ServiceDetails");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            try
            {
                var service = await _serviceDetails.GetServiceDetailsByIdAsync(id);
                if (service == null)
                    return NotFound();

                return View(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading ServiceDetails: {Id}", id);
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewDetails(string id)
        {
            try
            {
                var service = await _serviceDetails.GetServiceDetailsByIdAsync(id);
                return PartialView("View", service ?? new ServiceDetailsDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading ServiceDetails view: {Id}", id);
                return PartialView("View", new ServiceDetailsDto());
            }
        }

        [AllowAnonymous]
        [HttpGet("ViewAsserviceCard/{id}")]
        public async Task<IActionResult> ViewAsserviceCard(string id)
        {
            try
            {
                var service = await _serviceDetails.GetServiceDetailsByIdAsync(id);
                if (service == null)
                    return NotFound();

                ViewData["Title"] = service.NameEn ?? "Service Details";
                ViewData["BreadcrumbItems"] = new List<(string, string)>
                {
                    ("Services", "/"),
                    (service.NameEn ?? "Service Details", "")
                };

                return View("ServiceCard", service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading ServiceDetails view: {Id}", id);
                return NotFound();
            }
        }
    }
}
