using Services.Auth.Application.Models.Dto.Token;
using Services.Auth.Domain.Entities;

namespace Services.Auth.Application.IService
{
    public interface ITokenService
    {
        Task<AuthenticationDto> Authentication(ApplicationUser applicationUser);
        Task<JWTTokenDto> GenerateToken(ApplicationUser applicationUser);
        Task<RefreshTokenDto> GenerateRefreshToken();
        Task<AuthenticationDto> RefreshToken(AuthenticationDto authenticationDto);
        Task RevokeToken(string Token);
    }
}
