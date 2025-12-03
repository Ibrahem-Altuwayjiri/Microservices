using Microsoft.AspNetCore.Mvc;
using Services.ServicesManagement.Application.IService.ServiceStructure;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using Services.ServicesManagement.Application.Models.Abstract;
using System.Threading.Tasks;
using System.Collections.Generic;
using Services.ServicesManagement.API.Controllers.Base;

namespace Services.ServicesManagement.API.Controllers.ServiceStructure
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubServiceController : BaseApiController
    {
        private readonly ISubServiceService _subServiceService;

        public SubServiceController(ISubServiceService subServiceService)
        {
            _subServiceService = subServiceService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] string filter = "", [FromQuery] PaginationParametersDto? Pagination = null)
        {
            _response.Result = await _subServiceService.getAll(filter, Pagination);
            return Ok(_response);
        }

        [HttpGet("getAllWithAudit")]
        public async Task<IActionResult> GetAllWithAudit([FromQuery] string filter = "", [FromQuery] PaginationParametersDto? Pagination = null)
        {
            _response.Result = await _subServiceService.getAllWithAudit(filter, Pagination);
            return Ok(_response);
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            _response.Result = await _subServiceService.getById(Id);
            return Ok(_response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateSubServiceDto model)
        {
            if (!ValidateModel())
                return BadRequest(_response);

            _response.Result = await _subServiceService.create(model);
            return Ok(_response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateSubServiceDto model)
        {
            if (!ValidateModel())
                return BadRequest(_response);

            _response.Result = await _subServiceService.update(model);
            return Ok(_response);
        }

        [HttpPost("activate/{Id}")]
        public async Task<IActionResult> Activate(string Id)
        {
            _response.Result = await _subServiceService.activate(Id);
            return Ok(_response);
        }

        [HttpPost("deactivate/{Id}")]
        public async Task<IActionResult> Deactivate(string Id)
        {
            _response.Result = await _subServiceService.deactivate(Id);
            return Ok(_response);
        }
    }
}
