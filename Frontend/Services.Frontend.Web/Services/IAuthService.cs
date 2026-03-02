using Services.Frontend.Web.Models.Auth;

namespace Services.Frontend.Web.Services
{
    public interface IAuthService
    {
        Task<OtpResponse> LoginAsync(AuthLoginRequest request);
        Task<AuthenticationResponse> RefreshTokenAsync(string refreshToken);
        Task RevokeTokenAsync(string token);
        Task<AuthenticationResponse> VerifyOtpAsync(string otp, string accessKey);
        Task<AuthenticationResponse> VerifyOtpWithTokenAsync(string otp, string accessKey);
        Task ResendOtpAsync(string email);
    }

    public class AuthenticationResponse
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpires { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
    }

    public class AuthLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class OtpResponse
    {
        public string AccessKey { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    public class OtpVerification
    {
        public string Otp { get; set; }
        public string AccessKey { get; set; }
    }
}
