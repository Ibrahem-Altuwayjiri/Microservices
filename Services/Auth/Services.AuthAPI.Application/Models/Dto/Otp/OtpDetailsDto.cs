using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Application.Models.Dto.Otp
{
    public class OtpDetailsDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string HashedOtp { get; set; }
        public string AccessKey { get; set; }
        public string RequestIp { get; set; } 
        public bool IsUsed { get; set; }
        public int AttemptCount { get; set; } 
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsExpired { get; set; }
        public bool IsValid { get; set; }
    }
}
