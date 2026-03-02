using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Frontend.Web.Services.LookupService;
using Services.Frontend.Web.Services.DTOs.LookupService;

namespace Services.Frontend.Web.Controllers
{
    [Authorize]
    public class DomainsController : Controller
    {
        private readonly IDomainsService _domainsService;
        private readonly ILogger<DomainsController> _logger;

        public DomainsController(IDomainsService domainsService, ILogger<DomainsController> logger)
        {
            _domainsService = domainsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
                var domains = await _domainsService.GetDomainsAsync();
            return PartialView("List", domains);
        }

        [HttpGet]
        public async Task<IActionResult> Form(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var domain = await _domainsService.GetDomainsByIdAsync(id.Value);
                return PartialView("Form", new CreateOrUpdateDomainsDto
                {
                    NameAr = domain.NameAr,
                    NameEn = domain.NameEn,
                    Id = domain.Id
                });
            }
            return PartialView("Form", null);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateDomainsDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Domain creation validation failed");
                    return BadRequest(new { isSuccess = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                }

                _logger.LogInformation("Creating Domain: {NameEn}", dto.NameEn);
            var result = await _domainsService.CreateDomainsAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "Domain created successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Domain");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdateDomainsDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Domain update validation failed");
                    return BadRequest(new { isSuccess = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                }

                _logger.LogInformation("Updating Domain: {Id}", dto.Id);
            var result = await _domainsService.UpdateDomainsAsync(dto);
                return Ok(new { isSuccess = true, result = result, message = "Domain updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Domain");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting Domain: {Id}", id);
                var result = await _domainsService.DeactivateDomain(id);
                if (result)
                    return Ok(new { isSuccess = true, message = "Domain deleted successfully" });
                return BadRequest(new { isSuccess = false, message = "An error occurred while deleting the Domain." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Domain");
                return BadRequest(new { isSuccess = false, message = ex.Message });
            }
        }
    }
}
