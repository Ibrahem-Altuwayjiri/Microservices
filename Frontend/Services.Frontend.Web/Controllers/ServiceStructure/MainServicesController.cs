using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Frontend.Web.Services.ManageServices;
using Services.Frontend.Web.Services.DTOs.ManageServices;
using Services.Frontend.Web.Models.Dto;

namespace Services.Frontend.Web.Controllers
{
    [Authorize]
    public class MainServicesController : Controller
    {
        private readonly IMainServicesService _mainService;
        private readonly ILogger<MainServicesController> _logger;

        public MainServicesController(
            IMainServicesService mainService,
            ILogger<MainServicesController> logger)
        {
            _mainService = mainService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var services = await _mainService.GetMainServicesAsync();
                return PartialView("List", services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching MainServices");
                return PartialView("List", new List<MainServiceDto>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Form(string? id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                var service = await _mainService.GetMainServiceByIdAsync(id);
                    if (service != null)
                    {
                        var updateDto = new CreateOrUpdateMainServiceDto
                        {
                            Id = service.Id,
                            NameAr = service.NameAr,
                            NameEn = service.NameEn
                        };
                        return PartialView("Form", updateDto);
                    }
                }
                return PartialView("Form", null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading MainService form");
                return PartialView("Form", null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateMainServiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("MainService creation validation failed");
                    return BadRequest(new
                    {
                        isSuccess = false,
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                _logger.LogInformation("Creating MainService: {Name}", dto.NameAr);
            var result = await _mainService.CreateMainServiceAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "MainService created successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating MainService");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdateMainServiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("MainService update validation failed");
                    return BadRequest(new
                    {
                        isSuccess = false,
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                _logger.LogInformation("Updating MainService: {Id}", dto.Id);
            var result = await _mainService.UpdateMainServiceAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "MainService updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating MainService");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                _logger.LogInformation("Deleting MainService: {Id}", id);
                var result = await _mainService.DeactivateMainService(id);
                if (result)
                    return Ok(new { isSuccess = true, message = "MainService deleted successfully" });
                else
                    return BadRequest(new { isSuccess = false, message = "Failed to delete MainService" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting MainService");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }
    }
}
