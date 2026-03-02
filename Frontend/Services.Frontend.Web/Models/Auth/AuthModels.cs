namespace Services.Frontend.Web.Models.Auth
{
    /// <summary>
    /// Login request model for API authentication
    /// </summary>
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// OTP verification request model
    /// </summary>
    public class OtpVerificationRequest
    {
        public string Otp { get; set; }
    }

    /// <summary>
    /// Resend OTP request model
    /// </summary>
    public class ResendOtpRequest
    {
        public string Email { get; set; }
    }

    /// <summary>
    /// Login response model containing user authentication details
    /// </summary>
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public LoginResult Result { get; set; }
    }

    /// <summary>
    /// Login result containing access key for OTP verification
    /// </summary>
    public class LoginResult
    {
        public string AccessKey { get; set; }
    }

    /// <summary>
    /// OTP verification response model
    /// </summary>
    public class OtpVerificationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public OtpVerificationResult Result { get; set; }
    }

    /// <summary>
    /// OTP verification result containing authentication tokens
    /// Used for storing token and refresh token in cookies after OTP verification
    /// </summary>
    public class OtpVerificationResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
