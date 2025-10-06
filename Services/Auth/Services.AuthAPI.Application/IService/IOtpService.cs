using Services.AuthAPI.Application.Models.Dto.Otp;
using Services.AuthAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Application.IService
{
    public interface IOtpService
    {
        Task<OtpResponseDto> GenerateOtp(ApplicationUser applicationUser);
        Task<bool> VerifyOTP(OtpVerificationDto otpVerificationDto);
    }
}
