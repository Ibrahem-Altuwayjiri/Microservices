using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Frontend.Web.Services.ManageServices;
using Services.Frontend.Web.Services.DTOs.ManageServices;
using Services.Frontend.Web.Models.Dto;

namespace Services.Frontend.Web.Controllers
{
    [Authorize]
    public class SubSubServicesController : Controller
    {
        private readonly ISubSubServicesService _subSubService;
        private readonly ISubServicesService _subService;
        private readonly ILogger<SubSubServicesController> _logger;

        public SubSubServicesController(
            ISubSubServicesService subSubService,
            ISubServicesService subService,
            ILogger<SubSubServicesController> logger)
        {
            _subSubService = subSubService;
            _subService = subService;
            _logger = logger;
        }

         [HttpGet]
        public async Task<IActionResult> List(string? subServiceId = null)
        {
            try
            {
                var services = await _subSubService.GetSubSubServicesAsync();

                if (!string.IsNullOrEmpty(subServiceId))
                    services = services.Where(s => s.SubServiceId == subServiceId).ToList();

                return PartialView("List", services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching SubSubServices");
                return PartialView("List", new List<SubSubServiceDto>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Form(string? id)
        {
            try
            {
            var subServices = await _subService.GetSubServicesAsync();
                ViewBag.SubServices = subServices;

                if (!string.IsNullOrEmpty(id))
                {
                var service = await _subSubService.GetSubSubServiceByIdAsync(id);
                    if (service != null)
                    {
                        var updateDto = new CreateOrUpdateSubSubServiceDto
                        {
                            Id = service.Id,
                            NameAr = service.NameAr,
                            NameEn = service.NameEn,
                            SubServiceId = service.SubServiceId
                        };
                        return PartialView("Form", updateDto);
                    }
                }
                return PartialView("Form", null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading SubSubService form");
                return PartialView("Form", null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateSubSubServiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("SubSubService creation validation failed");
                    return BadRequest(new
                    {
                        isSuccess = false,
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                _logger.LogInformation("Creating SubSubService: {NameAr}", dto.NameAr);
            var result = await _subSubService.CreateSubSubServiceAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "SubSubService created successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating SubSubService");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdateSubSubServiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("SubSubService update validation failed");
                    return BadRequest(new
                    {
                        isSuccess = false,
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                _logger.LogInformation("Updating SubSubService: {Id}", dto.Id);
            var result = await _subSubService.UpdateSubSubServiceAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "SubSubService updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating SubSubService");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPost]
         public async Task<IActionResult> Delete(string id)
        {
            try
            {
                _logger.LogInformation("Deleting SubSubService: {Id}", id);
                var result = await _subSubService.DeactivateSubSubService(id);
                if (result)
                    return Ok(new { isSuccess = true, message = "SubSubService deleted successfully" });
                else
                    return BadRequest(new { isSuccess = false, message = "Failed to delete SubSubService" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting SubSubService");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }
    }
}
