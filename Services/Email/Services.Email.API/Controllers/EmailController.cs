using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Email.Application.IService;
using Services.Email.Application.Job;
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
        private readonly EmailSenderJob _emailSenderJob;
        private ResponseDto _response;

        public EmailController(IEmailService emailService, EmailSenderJob emailSenderJob)
        {
            _emailService = emailService;
            _response = new ResponseDto();
            _emailSenderJob = emailSenderJob;
        }


        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] CreateEmailDetailsDto EmailDetails)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _emailService.CreateEmail(EmailDetails);

            await _emailSenderJob.EnqueueAsync();

            return Ok(_response);
        }
    }
}
