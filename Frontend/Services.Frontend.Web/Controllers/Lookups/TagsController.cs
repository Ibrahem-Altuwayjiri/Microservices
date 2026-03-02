using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Frontend.Web.Services.LookupService;
using Services.Frontend.Web.Services.DTOs.LookupService;

namespace Services.Frontend.Web.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private readonly ITagsService _tagsService;
        private readonly ILogger<TagsController> _logger;

        public TagsController(ITagsService tagsService, ILogger<TagsController> logger)
        {
            _tagsService = tagsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
                var tags = await _tagsService.GetTagsAsync();
            return PartialView("List", tags);
        }

        [HttpGet]
        public async Task<IActionResult> Form(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var tag = await _tagsService.GetTagsByIdAsync(id.Value);
                return PartialView("Form", tag);
            }
            return PartialView("Form", null);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateTagsDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Tag creation validation failed");
                    return BadRequest(new { isSuccess = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                }

                _logger.LogInformation("Creating Tag: {NameEn}", dto.NameEn);
            var result = await _tagsService.CreateTagsAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "Tag created successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Tag");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdateTagsDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Tag update validation failed");
                    return BadRequest(new { isSuccess = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                }

                _logger.LogInformation("Updating Tag: {Id}", dto.Id);
            var result = await _tagsService.UpdateTagsAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "Tag updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Tag");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting Tag: {Id}", id);
                var result = await _tagsService.DeactivateTag(id);
                if (result)
                    return Ok(new { isSuccess = true, message = "Tag deleted successfully" });
                return BadRequest(new { isSuccess = false, message = "An error occurred while deleting the Tag." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Tag");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }
    }
}
