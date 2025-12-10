using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesManagement.API.Controllers.Base;
using Services.ServicesManagement.Application.IService.Lookups;
using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Activities;
using System.Threading.Tasks;

namespace Services.ServicesManagement.API.Controllers.Lookups
{
    [Route("api/ServiceInfo/[controller]")]
    [ApiController]
    public class ActivitiesController : BaseApiController
    {
        private readonly IActivitiesService _activitiesService;

        public ActivitiesController(IActivitiesService activitiesService)
        {
            _activitiesService = activitiesService;
        }
        
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateActivitiesDto model)
        {
            if (!ValidateModel())
            {
                return BadRequest(_response);
            }
            _response.Result = await _activitiesService.create(model);
            return Ok(_response);

        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdateActivitiesDto model)
        {
            if (!ValidateModel())
            {
                return BadRequest(_response);
            }
            _response.Result = await _activitiesService.update(model);
            return Ok(_response);

        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParametersDto? Pagination = null)
        {
            _response.Result = await _activitiesService.getAll(Pagination);
            return Ok(_response);
        }

        [HttpGet("getAllWithAudit")]
        public async Task<IActionResult> GetAllWithAudit([FromQuery] PaginationParametersDto? Pagination = null)
        {
            _response.Result = await _activitiesService.getAllWithAudit(Pagination);
            return Ok(_response);
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            _response.Result = await _activitiesService.getById(Id);
            return Ok(_response);
        }

        [HttpPost("activate/{Id}")]
        public async Task<IActionResult> activate(int Id)
        {
            _response.Result = await _activitiesService.activate(Id);
            return Ok(_response);
        }

        [HttpPost("deactivate/{Id}")]
        public async Task<IActionResult> deactivate(int Id)
        {
            _response.Result = await _activitiesService.deactivate(Id);
            return Ok(_response);
        }
    }
}
