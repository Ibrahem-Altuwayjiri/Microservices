using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Frontend.Web.Services.LookupService;
using Services.Frontend.Web.Services.DTOs.LookupService;

namespace Services.Frontend.Web.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private readonly IActivitiesService _activitiesService;
        private readonly ILogger<ActivitiesController> _logger;

        public ActivitiesController(IActivitiesService activitiesService, ILogger<ActivitiesController> logger)
        {
            _activitiesService = activitiesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
                var activities = await _activitiesService.GetActivitiesAsync();
            return PartialView("List", activities);
        }

        [HttpGet]
        public async Task<IActionResult> Form(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var activity = await _activitiesService.GetActivitiesByIdAsync(id.Value);
                return PartialView("Form", activity);
            }
            return PartialView("Form", null);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateActivitiesDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Activity creation validation failed");
                    return BadRequest(new { isSuccess = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                }

                _logger.LogInformation("Creating Activity: {NameEn}", dto.NameEn);
            var result = await _activitiesService.CreateActivitiesAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "Activity created successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Activity");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdateActivitiesDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Activity update validation failed");
                    return BadRequest(new { isSuccess = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                }

                _logger.LogInformation("Updating Activity: {Id}", dto.Id);
            var result = await _activitiesService.UpdateActivitiesAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "Activity updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Activity");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting Activity: {Id}", id);
                var result = await _activitiesService.DeactivateActivity(id);
                if(result)
                    return Ok(new { isSuccess = true, message = "Activity deleted successfully" });
                return BadRequest(new { isSuccess = false, message = "An error occurred while deleting the activity." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Activity");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }
    }
}
