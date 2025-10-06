using Services.Auth.Application.Models.Dto;
using Services.Auth.Application.Models.Dto.Abstract;
using Services.Auth.Application.Models.Dto.Otp;


namespace Services.Auth.Application.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<ResponseDto> VerifyTowFA(OtpVerificationDto otpVerificationDto);
    }
}
