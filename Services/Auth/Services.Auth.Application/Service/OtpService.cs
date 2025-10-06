using Microsoft.AspNetCore.Http;
using Services.Auth.Application.IService;
using Services.Auth.Application.Models.Dto.Otp;
using Services.Auth.Domain.Entities;
using Services.Auth.Domain.IRepositories;
using Services.Auth.Infrastructure.Configuration.ExceptionHandlers;
using Services.Auth.Infrastructure.Helper;
using System.Security.Cryptography;
using System.Text;

namespace Services.Auth.Application.Service
{
    public class OtpService : IOtpService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;


        private readonly int OtpExpirationInMinutes = 2;
        private readonly int OtpLength = 6;

        public OtpService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OtpResponseDto> GenerateOtp(ApplicationUser applicationUser)
        {
            var optCode = GenerateOtpCode();
            var otpDetails = new OtpDetails
            {
                UserId = applicationUser.Id,
                HashedOtp = HashOtp(optCode),
                AccessKey = Guid.NewGuid().ToString(),
                RequestIp = IpHelper.GetClientIp(_httpContextAccessor.HttpContext),
                ExpiresAt = DateTime.UtcNow.AddMinutes(OtpExpirationInMinutes),
            };

            var otp = await _unitOfWork.OtpDetailsRepository.Add(otpDetails);
            await _unitOfWork.CompletedAsync();

            //TODO: sent otp by email

            return new OtpResponseDto
            {
                AccessKey = otp.AccessKey,
                ExpiresAt = otp.ExpiresAt,
            };
        }

        public string GenerateOtpCode()
        {
            Random random = new Random();

            return new string(Enumerable.Repeat("0123456789", OtpLength)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string HashOtp(string otp)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(otp);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public async Task<bool> VerifyOTP(OtpVerificationDto otpVerificationDto)
        {
            string clientIp = IpHelper.GetClientIp(_httpContextAccessor.HttpContext);
            string clintOtpHashed = HashOtp(otpVerificationDto.Otp.Trim());

            bool IsMatched = false;

            var otpInDB = await _unitOfWork.OtpDetailsRepository.FindOneOrDefault(o => o.AccessKey == otpVerificationDto.AccessKey 
                                                                                    && !o.IsUsed
                                                                                    && o.IsValid);

            if(otpInDB == null || otpInDB.IsExpired)
                throw new RestfulException("Invalid or expired OTP", RestfulStatusCodes.Forbidden);

            if (otpInDB.RequestIp != clientIp || otpInDB.AttemptCount > 3)
            {
                otpInDB.IsValid = false;
                IsMatched = false;
            }
            else
            {
                if (!otpInDB.HashedOtp.Equals(clintOtpHashed))
                {
                    otpInDB.AttemptCount += 1;
                    IsMatched = false;
                }
                else
                {
                    otpInDB.IsUsed = true;
                    IsMatched = true;
                }
            } 
               
            await _unitOfWork.OtpDetailsRepository.Update(otpInDB);
            await _unitOfWork.CompletedAsync();

            return IsMatched;

        }
    }
}
