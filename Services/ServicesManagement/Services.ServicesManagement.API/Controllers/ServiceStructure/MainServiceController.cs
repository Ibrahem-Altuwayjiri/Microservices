using Microsoft.AspNetCore.Mvc;
using Services.ServicesManagement.API.Controllers.Base;
using Services.ServicesManagement.Application.IService.ServiceStructure;
using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure.CreateOrUpdate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.ServicesManagement.API.Controllers.ServiceStructure
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainServiceController : BaseApiController
    {
        private readonly IMainServiceService _mainServiceService;

        public MainServiceController(IMainServiceService mainServiceService)
        {
            _mainServiceService = mainServiceService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] string filter = "", [FromQuery] PaginationParametersDto? Pagination = null)
        {
            _response.Result = await _mainServiceService.getAll(filter, Pagination);
            return Ok(_response);
        }

        [HttpGet("getAllWithAudit")]
        public async Task<IActionResult> GetAllWithAudit([FromQuery] string filter = "", [FromQuery] PaginationParametersDto? Pagination = null)
        {
            _response.Result = await _mainServiceService.getAllWithAudit(filter, Pagination);
            return Ok(_response);
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            _response.Result = await _mainServiceService.getById(Id);
            return Ok(_response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateMainServiceDto model)
        {
            if (!ValidateModel())
                return BadRequest(_response);

            _response.Result = await _mainServiceService.create(model);
            return Ok(_response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateMainServiceDto model)
        {
            if (!ValidateModel())
                return BadRequest(_response);

            _response.Result = await _mainServiceService.update(model);
            return Ok(_response);
        }

        [HttpPost("activate/{Id}")]
        public async Task<IActionResult> Activate(string Id)
        {
            _response.Result = await _mainServiceService.activate(Id);
            return Ok(_response);
        }

        [HttpPost("deactivate/{Id}")]
        public async Task<IActionResult> Deactivate(string Id)
        {
            _response.Result = await _mainServiceService.deactivate(Id);
            return Ok(_response);
        }
    }
}
