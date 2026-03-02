using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Frontend.Web.Services.ManageServices;
using Services.Frontend.Web.Services.LookupService;

namespace Services.Frontend.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMainServicesService _mainService;
        private readonly IServiceDetailsService _serviceDetails;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMainServicesService mainService, IServiceDetailsService serviceDetails, ILogger<HomeController> logger)
        {
            _mainService = mainService;
            _serviceDetails = serviceDetails;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        // SERVICE MANAGEMENT - Accessible to both authenticated and anonymous users
        [AllowAnonymous]
        public IActionResult ServiceManagement()
        {
            return View("Admin/ServiceManagement");
        }

        // USER ACTIONS
        [AllowAnonymous]
        public async Task<IActionResult> Services()
        {
            var services = await _serviceDetails.GetAllServiceDetailsAsync();
            return PartialView("User/BrowseServices", services);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ServiceDetail(string id)
        {
            var service = await _serviceDetails.GetServiceDetailsByIdAsync(id);
            if (service == null)
                return NotFound();

            return View(service);
        }

        // ADMIN ACTIONS
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
