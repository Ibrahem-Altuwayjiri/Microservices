using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Email.Application.IService;
using Services.Email.Application.Models.Abstract;
using Services.Email.Application.Models.Dto.EmailDetails;

namespace Services.Email.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private ResponseDto _response;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
            _response = new ResponseDto();

        }


        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] CreateEmailDetailsDto EmailDetails)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _emailService.CreateEmail(EmailDetails);
            return Ok(_response);
        }
    }
}
