using Microsoft.AspNetCore.Identity;
using Services.AuthAPI.Application.IService;
using Services.AuthAPI.Application.Models.Dto;
using Services.AuthAPI.Application.Models.Dto.Abstract;
using Services.AuthAPI.Application.Models.Dto.Otp;
using Services.AuthAPI.Domain.Entities;
using Services.AuthAPI.Domain.IRepositories;
using Services.AuthAPI.Infrastructure.Cache;
using Services.AuthAPI.Infrastructure.Configuration;
using Services.AuthAPI.Infrastructure.Configuration.ExceptionHandlers;
using System.DirectoryServices.AccountManagement;



namespace Services.AuthAPI.Application.Service
{
    public class AuthService : IAuthService
    {
        //private readonly PrincipalContext ActiveDirectoryContext;
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _jwtTokenGenerator;
        private readonly IOtpService _otpService;

        private readonly int OtpCodeLength = 4;
        private readonly int AllowedFailedAttempts;

        private readonly int OtpExpirationInSeconds;
        private readonly int LockoutPeriodInSeconds;

        private readonly string OtpCacheKeyPrefix = "OTP-";
        private readonly string FailedLoginAttemptCacheKeyPrefix = "FATT-";

        public AuthService(ITokenService jwtTokenGenerator,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOtpService otpService,
            IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager)
        {
            //ActiveDirectoryContext = new PrincipalContext(ContextType.Domain);


            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _otpService = otpService;
            _signInManager = signInManager;


            OtpExpirationInSeconds = ConfigurationUtil.GetValue<int>("OtpExpirationInSeconds");
            LockoutPeriodInSeconds = ConfigurationUtil.GetValue<int>("LockoutPeriodInSeconds");
            AllowedFailedAttempts = ConfigurationUtil.GetValue<int>("AllowedFailedAttempts");
        }

        public async Task<ResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var IsValid = IsValidloginRequest(loginRequestDto);
            if (IsValid != null)
                return new ResponseDto { IsSuccess = false , Message = IsValid};

            var user = await _unitOfWork.ApplicationUsersRepository.FindOneOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            if (user == null)
            {
                //TODO: add new user if user = null. using UserService
            }

            //bool isValidCredentials = ActiveDirectoryContext.ValidateCredentials(loginRequestDto.UserName, loginRequestDto.Password);
            var isValidCredentials = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, true);

            if (!isValidCredentials.Succeeded)
            {
                return new ResponseDto() { Message = "Username or password is incorrect", IsSuccess = false };
            }

            

            user.LastLogin = DateTime.Now.ToLocalTime();
            await _unitOfWork.ApplicationUsersRepository.Update(user);


            var otp = await _otpService.GenerateOtp(user);
            return new ResponseDto
            {
                Result = otp,
                IsSuccess = true,
            };
        }

        public async Task<ResponseDto> VerifyTowFA(OtpVerificationDto otpVerificationDto)
        {
            var isVilde = await _otpService.VerifyOTP(otpVerificationDto);

            if(!isVilde)
                throw new RestfulException("Invalid or expired OTP", RestfulStatusCodes.Forbidden);

            var otpDetails = await _unitOfWork.OtpDetailsRepository.FindOneOrDefault(o => o.AccessKey == otpVerificationDto.AccessKey);
            var user = await _unitOfWork.ApplicationUsersRepository.FindOneOrDefault(u => u.Id == otpDetails.UserId);

            var jwtToken = await _jwtTokenGenerator.Authentication(user);

            return new ResponseDto
            {
                Result = jwtToken,
                IsSuccess = true
            };
        }

        private bool IsAccountLocked(string email)
        {
            return CacheManager.ContainsKey(FailedLoginAttemptCacheKeyPrefix + email) && CacheManager.GetValue<int>(FailedLoginAttemptCacheKeyPrefix + email) >= AllowedFailedAttempts;
        }

        private string? IsValidloginRequest(LoginRequestDto loginRequestDto)
        {
            if (loginRequestDto.UserName == null)
                return "Incomplete Or Bad Request";
            if (IsAccountLocked(loginRequestDto.UserName))
                return "Account Is Locked";

            return null;

        }

        //TODO: disAllow the concurrent user
    }
}
