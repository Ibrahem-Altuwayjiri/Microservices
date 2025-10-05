using Services.AuthAPI.Domain.Entities;
using Services.AuthAPI.Shared.Models.Dto.Token;

namespace Services.AuthAPI.Application.IService
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
