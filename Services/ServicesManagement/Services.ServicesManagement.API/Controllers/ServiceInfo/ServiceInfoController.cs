using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesManagement.API.Controllers.Base;
using Services.ServicesManagement.Application.IService.ServiceInfo;
using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.ServiceInfo.CreateOrUpdate;
using Services.ServicesManagement.Application.Models.Dto.ServiceInfo;
using System.Threading.Tasks;

namespace Services.ServicesManagement.API.Controllers.ServiceInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceInfoController : BaseApiController
    {
        private readonly IServiceInfoService _serviceInfoService;

        public ServiceInfoController(IServiceInfoService serviceInfoService)
        {
            _serviceInfoService = serviceInfoService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateServiceDetailsDto model)
        {
            if (!ValidateModel())
                return BadRequest(_response);

            _response.Result = await _serviceInfoService.create(model);
            return Ok(_response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateServiceDetailsDto model)
        {
            if (!ValidateModel())
                return BadRequest(_response);

            _response.Result = await _serviceInfoService.update(model);
            return Ok(_response);
        }

        [HttpPost("activate/{Id}")]
        public async Task<IActionResult> Activate(string Id)
        {
            _response.Result = await _serviceInfoService.activate(Id);
            return Ok(_response);
        }

        [HttpPost("deactivate/{Id}")]
        public async Task<IActionResult> Deactivate(string Id)
        {
            _response.Result = await _serviceInfoService.deactivate(Id);
            return Ok(_response);
        }

        [HttpGet("getById/{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            _response.Result = await _serviceInfoService.getById(Id);
            return Ok(_response);
        }

        [HttpPost("getAll")]
        public async Task<IActionResult> GetAll([FromBody] ServiceDetailsFilteringDto? filtering)
        {


            _response.Result = await _serviceInfoService.getAll(filtering, filtering?.pagination);
            return Ok(_response);
        }

        [HttpPost("getAllWithAudit")]
        public async Task<IActionResult> GetAllWithAudit([FromBody] ServiceDetailsFilteringDto? filtering)
        {


            _response.Result = await _serviceInfoService.getAllWithAudit(filtering, filtering?.pagination);
            return Ok(_response);
        }
    }
}
