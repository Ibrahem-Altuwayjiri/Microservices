using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Auth.Application.IService;
using Services.Auth.Application.Models.Dto;
using Services.Auth.Application.Models.Dto.Otp;
using Services.Auth.Application.Models.Dto.Token;


namespace Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IOtpService _otpService;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, IOtpService otpService, ITokenService tokenService)
        {
            _authService = authService;
            _otpService = otpService;
            _tokenService = tokenService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            return Ok(await _authService.Login(loginRequest));
        }

        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP([FromBody] OtpVerificationDto otpVerification)
        {
            
            return Ok(await _authService.VerifyTowFA(otpVerification));
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] AuthenticationDto authentication)
        {
            return Ok(await _tokenService.RefreshToken(authentication));
        }

        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] string token)
        {
            await _tokenService.RevokeToken(token);
            return NoContent();
        }
    }
}
