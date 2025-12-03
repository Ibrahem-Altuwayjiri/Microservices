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
    public class SubSubServiceController : BaseApiController
    {
        private readonly ISubSubServiceService _subSubServiceService;

        public SubSubServiceController(ISubSubServiceService subSubServiceService)
        {
            _subSubServiceService = subSubServiceService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] string filter = "", [FromQuery] PaginationParametersDto? Pagination = null)
        {
            _response.Result = await _subSubServiceService.getAll(filter, Pagination);
            return Ok(_response);
        }

        [HttpGet("getAllWithAudit")]
        public async Task<IActionResult> GetAllWithAudit([FromQuery] string filter = "", [FromQuery] PaginationParametersDto? Pagination = null)
        {
            _response.Result = await _subSubServiceService.getAllWithAudit(filter, Pagination);
            return Ok(_response);
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            _response.Result = await _subSubServiceService.getById(Id);
            return Ok(_response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateSubSubServiceDto model)
        {
            if (!ValidateModel())
                return BadRequest(_response);

            _response.Result = await _subSubServiceService.create(model);
            return Ok(_response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateSubSubServiceDto model)
        {
            if (!ValidateModel())
                return BadRequest(_response);

            _response.Result = await _subSubServiceService.update(model);
            return Ok(_response);
        }

        [HttpPost("activate/{Id}")]
        public async Task<IActionResult> Activate(string Id)
        {
            _response.Result = await _subSubServiceService.activate(Id);
            return Ok(_response);
        }

        [HttpPost("deactivate/{Id}")]
        public async Task<IActionResult> Deactivate(string Id)
        {
            _response.Result = await _subSubServiceService.deactivate(Id);
            return Ok(_response);
        }
    }
}
