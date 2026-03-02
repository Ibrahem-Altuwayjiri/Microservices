using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Frontend.Web.Services.LookupService;
using Services.Frontend.Web.Services.DTOs.LookupService;

namespace Services.Frontend.Web.Controllers
{
    [Authorize]
    public class HeadersController : Controller
    {
        private readonly IHeadersService _headersService;
        private readonly ILogger<HeadersController> _logger;

        public HeadersController(IHeadersService headersService, ILogger<HeadersController> logger)
        {
            _headersService = headersService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
                var headers = await _headersService.GetHeadersAsync();
            return PartialView("List", headers);
        }

        [HttpGet]
        public async Task<IActionResult> Form(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var header = await _headersService.GetHeadersByIdAsync(id.Value);
                return PartialView("Form", header);
            }
            return PartialView("Form", null);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateHeaderDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Header creation validation failed");
                    return BadRequest(new { isSuccess = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                }

                _logger.LogInformation("Creating Header: {NameEn}", dto.NameEn);
            var result = await _headersService.CreateHeadersAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "Header created successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Header");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdateHeaderDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Header update validation failed");
                    return BadRequest(new { isSuccess = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                }

                _logger.LogInformation("Updating Header: {Id}", dto.Id);
            var result = await _headersService.UpdateHeadersAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "Header updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Header");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting Header: {Id}", id);
                var result = await _headersService.DeactivateHeader(id);
                if (result)
                    return Ok(new { isSuccess = true, message = "Header deleted successfully" });
                return BadRequest(new { isSuccess = false, message = "An error occurred while deleting the Header." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Header");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }
    }
}
