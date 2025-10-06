using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.AuthAPI.Application.IService;
using Services.AuthAPI.Application.Models.Dto.Token;
using Services.AuthAPI.Domain.Entities;
using Services.AuthAPI.Domain.IRepositories;
using Services.AuthAPI.Infrastructure.Configuration;
using Services.AuthAPI.Infrastructure.Configuration.ExceptionHandlers;
using Services.AuthAPI.Infrastructure.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Services.AuthAPI.Application.Service
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string JWTAudience;
        private readonly string JWTIssuer;
        private readonly string JWTSecret;

        private readonly int TokenExpirationInMinutes = 10;
        private readonly int RefreshTokenExpirationInHours = 10;

        public TokenService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;

            JWTAudience = ConfigurationUtil.GetValue<string>("JWT:Audience");
            JWTIssuer = ConfigurationUtil.GetValue<string>("JWT:Issuer");
            JWTSecret = ConfigurationUtil.GetValue<string>("JWT:Secret");
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<AuthenticationDto> Authentication(ApplicationUser applicationUser)
        {
            JWTTokenDto jwtToken = await GenerateToken(applicationUser);
            RefreshTokenDto refreshToken = await GenerateRefreshToken();

            var response = new AuthenticationDto
            {
                UserId = applicationUser.Id,
                Token = jwtToken.Token,
                TokenExpires = jwtToken.TokenExpires,
                RefreshToken = refreshToken.RefreshToken,
                RefreshTokenExpires = refreshToken.RefreshTokenExpires
            };
            await SaveToken(response);
            return response;
        }

        public async Task<JWTTokenDto> GenerateToken(ApplicationUser applicationUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = GetClaims(applicationUser);
            var key = Encoding.ASCII.GetBytes(JWTSecret);
            var expires = DateTime.UtcNow.AddMinutes(TokenExpirationInMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = JWTAudience,
                Issuer = JWTIssuer,
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return new JWTTokenDto
            {
                Token = token,
                TokenExpires = expires
            };
            //var response = new AuthenticationDto
            //{
            //    Token = token,
            //    TokenExpires = DateTime.UtcNow.AddMinutes(TokenExpirationInMinutes),
            //    RefreshToken = GenerateRefreshToken(),
            //    RefreshTokenExpires = DateTime.UtcNow.AddHours(RefreshTokenExpirationInHours)
            //};
            //await SaveToken(response);

            //return response;
        }

        public async Task<RefreshTokenDto> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                return new RefreshTokenDto
                {
                    RefreshToken = Convert.ToBase64String(randomNumber),
                    RefreshTokenExpires = DateTime.UtcNow.AddHours(RefreshTokenExpirationInHours)
                };
            }
        }


        private async Task SaveToken(AuthenticationDto createToken)
        {
            var token = new TokenDetails
            {
                UserId = createToken.UserId,
                Token = createToken.Token,
                TokenExpires = createToken.TokenExpires,
                RefreshToken = createToken.RefreshToken,
                RefreshTokenExpires = createToken.RefreshTokenExpires,
                CreateDate = DateTime.UtcNow,
                CreateByDeviceIP = IpHelper.GetClientIp(_httpContextAccessor.HttpContext),
                NumberOfUpdate = 0
            };
            await _unitOfWork.TokenDetailsRepository.Add(token);
            await _unitOfWork.CompletedAsync();
        }


        private List<Claim> GetClaims(ApplicationUser applicationUser)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub,applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name,applicationUser.FullName)

            };
        }

        

        public async Task<AuthenticationDto> RefreshToken(AuthenticationDto authenticationDto)
        {
            var clientDeviceIP = IpHelper.GetClientIp(_httpContextAccessor.HttpContext);
            var TokenDetails = await _unitOfWork.TokenDetailsRepository.FindOneOrDefault(m => m.Token == authenticationDto.Token 
                                                                                    && m.RefreshToken == authenticationDto.RefreshToken
                                                                                    && m.CreateByDeviceIP == clientDeviceIP);
            if(TokenDetails == null)
                throw new RestfulException("Bad Request", RestfulStatusCodes.BadRequest);
            if(!TokenDetails.IsActive)
                throw new RestfulException("Invalid or expired token.", RestfulStatusCodes.Forbidden);

            var applicationUser = await _unitOfWork.ApplicationUsersRepository.FindOneOrDefault(u => u.Id == authenticationDto.UserId);
            if (applicationUser == null)
                throw new RestfulException("Bad Request", RestfulStatusCodes.BadRequest);

            JWTTokenDto jwtToken = await GenerateToken(applicationUser);

            TokenDetails.Token = jwtToken.Token;
            TokenDetails.TokenExpires = jwtToken.TokenExpires;
            TokenDetails.LastUpdateDate = DateTime.Now.ToLocalTime();
            TokenDetails.NumberOfUpdate += 1;

            await _unitOfWork.TokenDetailsRepository.Update(TokenDetails);
            await _unitOfWork.CompletedAsync();

            authenticationDto.Token = jwtToken.Token;
            authenticationDto.TokenExpires = jwtToken.TokenExpires;

            return authenticationDto;

        }

        public async Task RevokeToken(string Token)
        {
            var clientDeviceIP = IpHelper.GetClientIp(_httpContextAccessor.HttpContext);
            var TokenDetails = await _unitOfWork.TokenDetailsRepository.FindOneOrDefault(m => m.Token == Token
                                                                                    && m.CreateByDeviceIP == clientDeviceIP);
            if (TokenDetails == null)
                throw new RestfulException("Bad Request", RestfulStatusCodes.BadRequest);

            TokenDetails.RevokedDate = DateTime.Now.ToLocalTime();
            await _unitOfWork.TokenDetailsRepository.Update(TokenDetails); 
            await _unitOfWork.CompletedAsync();
        }
    }
}
