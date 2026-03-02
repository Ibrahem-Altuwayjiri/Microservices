using Services.Frontend.Web.Configuration.ExceptionHandlers;
using Services.Frontend.Web.Models.Dto;
using Services.Frontend.Web.Models.Auth;
using Services.Frontend.Web.Enum;

namespace Services.Frontend.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IBaseService baseService, ILogger<AuthService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<OtpResponse> LoginAsync(AuthLoginRequest request)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/Auth/Login",
                Data = request,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto, withBearer: false);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Login failed",
                    RestfulStatusCodes.Unauthorized);
            }

            return System.Text.Json.JsonSerializer.Deserialize<OtpResponse>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<AuthenticationResponse> VerifyOtpAsync(string otp, string accessKey)
        {
            var request = new { otp, accessKey };
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/Auth/VerifyOTP",
                Data = request,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto, withBearer: false);
            if (response?.IsSuccess != true)
            {
                throw new RestfulException(
                    response?.Message ?? "OTP verification failed",
                    RestfulStatusCodes.BadRequest);
            }

            return System.Text.Json.JsonSerializer.Deserialize<AuthenticationResponse>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true }); ;
        }

        // ? NEW METHOD: Verify OTP and get token response with refresh token
        public async Task<AuthenticationResponse> VerifyOtpWithTokenAsync(string otp, string accessKey)
        {
            var request = new { otp, accessKey };
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/Auth/VerifyOTP",
                Data = request,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto, withBearer: false);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "OTP verification failed",
                    RestfulStatusCodes.BadRequest);
            }

            return System.Text.Json.JsonSerializer.Deserialize<AuthenticationResponse>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<AuthenticationResponse> RefreshTokenAsync(string refreshToken)
        {
            var request = new { token = refreshToken };
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/Auth/RefreshToken",
                Data = request,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto, withBearer: false);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Token refresh failed",
                    RestfulStatusCodes.Unauthorized);
            }

            return System.Text.Json.JsonSerializer.Deserialize<AuthenticationResponse>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task RevokeTokenAsync(string token)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/Auth/RevokeToken",
                Data = token,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto, withBearer: true);
            if (response?.IsSuccess != true)
            {
                throw new RestfulException(
                    response?.Message ?? "Token revocation failed",
                    RestfulStatusCodes.InternalServerError);
            }
        }

        public async Task ResendOtpAsync(string email)
        {
            var request = new { email };
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/Auth/ResendOTP",
                Data = request,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto, withBearer: false);
            if (response?.IsSuccess != true)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to resend OTP",
                    RestfulStatusCodes.BadRequest);
            }
        }
    }
}
