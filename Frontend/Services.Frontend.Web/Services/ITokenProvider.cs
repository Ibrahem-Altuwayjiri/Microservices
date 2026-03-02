namespace Services.Frontend.Web.Services
{
    public interface ITokenProvider
    {
        // JWT Token Management
        void SetToken(string token);
        string? GetToken();
        void ClearToken();

        // Refresh Token Management
        void SetRefreshToken(string refreshToken);
        string? GetRefreshToken();
        void ClearRefreshToken();

        // Clear All Tokens
        void ClearAllTokens();
    }
}
