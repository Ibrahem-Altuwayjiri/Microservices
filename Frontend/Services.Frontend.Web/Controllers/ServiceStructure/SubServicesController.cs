using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Frontend.Web.Services.ManageServices;
using Services.Frontend.Web.Services.DTOs.ManageServices;
using Services.Frontend.Web.Models.Dto;

namespace Services.Frontend.Web.Controllers
{
    [Authorize]
    public class SubServicesController : Controller
    {
        private readonly ISubServicesService _subService;
        private readonly IMainServicesService _mainService;
        private readonly ILogger<SubServicesController> _logger;

        public SubServicesController(
            ISubServicesService subService,
            IMainServicesService mainService,
            ILogger<SubServicesController> logger)
        {
            _subService = subService;
            _mainService = mainService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List(string? mainServiceId = null)
        {
            try
            {
            var services = await _subService.GetSubServicesAsync();

                if (!string.IsNullOrEmpty(mainServiceId))
                    services = services.Where(s => s.MainServiceId == mainServiceId).ToList();

                return PartialView("List", services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching SubServices");
                return PartialView("List", new List<SubServiceDto>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Form(string? id)
        {
            try
            {
                // Get main services for dropdown
            var mainServices = await _mainService.GetMainServicesAsync();
                ViewBag.MainServices = mainServices;

                if (!string.IsNullOrEmpty(id))
                {
                var service = await _subService.GetSubServiceByIdAsync(id);
                    if (service != null)
                    {
                        var updateDto = new CreateOrUpdateSubServiceDto
                        {
                            Id = service.Id,
                            NameAr = service.NameAr,
                            NameEn = service.NameEn,
                            MainServiceId = service.MainServiceId
                        };
                        return PartialView("Form", updateDto);
                    }
                }
                return PartialView("Form", null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading SubService form");
                return PartialView("Form", null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateSubServiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("SubService creation validation failed");
                    return BadRequest(new
                    {
                        isSuccess = false,
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                _logger.LogInformation("Creating SubService: {NameAr}", dto.NameAr);
            var result = await _subService.CreateSubServiceAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "SubService created successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating SubService");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdateSubServiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("SubService update validation failed");
                    return BadRequest(new
                    {
                        isSuccess = false,
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                _logger.LogInformation("Updating SubService: {Id}", dto.Id);
            var result = await _subService.UpdateSubServiceAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "SubService updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating SubService");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                _logger.LogInformation("Deleting SubService: {Id}", id);
                var result = await _subService.DeactivateSubService(id);
                if (result)
                    return Ok(new { isSuccess = true, message = "SubService deleted successfully" });
                else
                    return BadRequest(new { isSuccess = false, message = "Failed to delete SubService" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting SubService");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }
    }
}
