using Services.AuthAPI.Application.Models.Dto;
using Services.AuthAPI.Application.Models.Dto.Abstract;
using Services.AuthAPI.Application.Models.Dto.Otp;


namespace Services.AuthAPI.Application.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<ResponseDto> VerifyTowFA(OtpVerificationDto otpVerificationDto);
    }
}
