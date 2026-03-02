using Services.Frontend.Web.Enum;

namespace Services.Frontend.Web.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        // ✅ JWT TOKEN MANAGEMENT

        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
        }

        // ✅ REFRESH TOKEN MANAGEMENT

        public void SetRefreshToken(string refreshToken)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append("RefreshToken", refreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });
        }

        public string? GetRefreshToken()
        {
            string? refreshToken = null;
            bool? hasRefreshToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue("RefreshToken", out refreshToken);
            return hasRefreshToken is true ? refreshToken : null;
        }

        public void ClearRefreshToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete("RefreshToken");
        }

        // ✅ CLEAR ALL TOKENS (Logout)

        public void ClearAllTokens()
        {
            ClearToken();
            ClearRefreshToken();
        }
    }
}
