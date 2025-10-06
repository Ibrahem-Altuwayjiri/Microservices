using Services.AuthAPI.Application.Models.Dto.Token;
using Services.AuthAPI.Domain.Entities;

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
