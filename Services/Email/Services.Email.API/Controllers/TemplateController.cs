using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Email.Application.IService;
using Services.Email.Application.Models.Abstract;
using Services.Email.Application.Models.Dto.Template;

namespace Services.Email.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TemplateController : ControllerBase
    {

        private readonly ITemplateService _templateService;
        private ResponseDto _response;

        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
            _response = new ResponseDto();
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
                return BadRequest();

            _response.Result = await _templateService.GetTemplate(id);
            return Ok(_response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _response.Result = await _templateService.GetAllTemplates();
            return Ok(_response);
        }

        [HttpPost("CreateTemplate")]
        public async Task<IActionResult> CreateTemplate([FromBody] CreateTemplateDto Template)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _response.Result = await _templateService.CreateTemplate(Template);
            return Ok(_response);
        }

        [HttpPost("UpdateTemplate")]
        public async Task<IActionResult> UpdateTemplate([FromBody] UpdateTemplateDto UpdateTemplate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _response.Result = await _templateService.UpdateTemplate(UpdateTemplate);
            return Ok(_response);
        }
    }
}
