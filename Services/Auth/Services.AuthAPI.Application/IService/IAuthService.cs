using Services.AuthAPI.Shared.Models.Dto;
using Services.AuthAPI.Shared.Models.Dto.Abstract;
using Services.AuthAPI.Shared.Models.Dto.Otp;
using Services.AuthAPI.Shared.Models.Dto.Token;

namespace Services.AuthAPI.Application.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<ResponseDto> VerifyTowFA(OtpVerificationDto otpVerificationDto);
    }
}
