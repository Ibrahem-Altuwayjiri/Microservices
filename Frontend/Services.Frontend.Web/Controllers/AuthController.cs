using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Services.Frontend.Web.Models.Auth;
using Services.Frontend.Web.Services;
using System.Security.Claims;

namespace Services.Frontend.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAuthService authService,
            ITokenProvider tokenProvider,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
            _logger = logger;
        }

        /// <summary>
        /// Display login page
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated ?? false)
                return RedirectToAction("ServiceManagement", "Home");

            return View();
        }

        /// <summary>
        /// Handle login request with email and password
        /// Returns access key for OTP verification
        /// </summary>
        [HttpPost]
        [Route("api/auth/login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var response = await _authService.LoginAsync(new AuthLoginRequest
            {
                UserName = request.Email,
                Password = request.Password
            });

            HttpContext.Session.SetString("tempAccessKey", response.AccessKey);
            HttpContext.Session.SetString("tempEmail", request.Email);

            return Ok(new
            {
                isSuccess = true,
                result = new { ExpiresAt = response.ExpiresAt },
                message = "Login successful. Please verify OTP."
            });
        }

        /// <summary>
        /// Verify OTP and issue authentication tokens
        /// </summary>
        [HttpPost]
        [Route("api/auth/verify-otp")]
        public async Task<IActionResult> VerifyOtpAsync([FromBody] OtpVerificationRequest request)
        {
            var accessKey = HttpContext.Session.GetString("tempAccessKey");
            var email = HttpContext.Session.GetString("tempEmail");

            // Verify OTP and get token response
            var otpResponse = await _authService.VerifyOtpWithTokenAsync(request.Otp, accessKey);

            // ? STORE TOKEN IN SECURE COOKIES FOR API REQUESTS
            if (!string.IsNullOrEmpty(otpResponse.Token))
            {
                _tokenProvider.SetToken(otpResponse.Token);
            }

            // ? STORE REFRESH TOKEN IN SECURE COOKIES FOR TOKEN REFRESH
            if (!string.IsNullOrEmpty(otpResponse.RefreshToken))
            {
                _tokenProvider.SetRefreshToken(otpResponse.RefreshToken);
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, accessKey),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IssuedUtc = DateTimeOffset.UtcNow,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                IsPersistent = false
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            HttpContext.Session.Remove("tempAccessKey");
            HttpContext.Session.Remove("tempEmail");

            return Ok(new
            {
                isSuccess = true,
                result = new
                {
                    token = otpResponse.Token,
                    refreshToken = otpResponse.RefreshToken,
                    userName = email,
                    email = email
                },
                message = "OTP verified successfully"
            });
        }

        /// <summary>
        /// Resend OTP to user email
        /// </summary>
        [HttpPost]
        [Route("api/auth/resend-otp")]
        public async Task<IActionResult> ResendOtpAsync([FromBody] ResendOtpRequest request)
        {
            await _authService.ResendOtpAsync(request.Email);

            return Ok(new
            {
                isSuccess = true,
                message = "OTP sent successfully"
            });
        }

        /// <summary>
        /// Logout user
        /// </summary>
        [Route("Auth/Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            // ? CLEAR ALL TOKEN COOKIES ON LOGOUT (using new ClearAllTokens method)
            _tokenProvider.ClearAllTokens();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
