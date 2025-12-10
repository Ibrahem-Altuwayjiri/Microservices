using Microsoft.AspNetCore.Mvc;
using Services.ServicesManagement.Application.IService.Lookups;
using Services.ServicesManagement.Application.Models.Dto.Tags;
using Services.ServicesManagement.Application.Models.Abstract;
using System.Threading.Tasks;
using Services.ServicesManagement.API.Controllers.Base;

namespace Services.ServicesManagement.API.Controllers.Lookups
{
    [Route("api/ServiceInfo/[controller]")]
    [ApiController]
    public class TagsController : BaseApiController
    {
        private readonly ITagsService _tagsService;

        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParametersDto? Pagination = null)
        {
            _response.Result = await _tagsService.getAll(Pagination);
            return Ok(_response);
        }

        [HttpGet("getAllWithAudit")]
        public async Task<IActionResult> GetAllWithAudit([FromQuery] PaginationParametersDto? Pagination = null)
        {
            _response.Result = await _tagsService.getAllWithAudit(Pagination);
            return Ok(_response);
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            _response.Result = await _tagsService.getById(Id);
            return Ok(_response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateTagsDto model)
        {
            if (!ValidateModel())
                return BadRequest(_response);

            _response.Result = await _tagsService.create(model);
            return Ok(_response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdateTagsDto model)
        {
            if (!ValidateModel())
                return BadRequest(_response);

            _response.Result = await _tagsService.update(model);
            return Ok(_response);
        }

        [HttpPost("activate/{Id}")]
        public async Task<IActionResult> Activate(int Id)
        {
            _response.Result = await _tagsService.activate(Id);
            return Ok(_response);
        }

        [HttpPost("deactivate/{Id}")]
        public async Task<IActionResult> Deactivate(int Id)
        {
            _response.Result = await _tagsService.deactivate(Id);
            return Ok(_response);
        }
    }
}
