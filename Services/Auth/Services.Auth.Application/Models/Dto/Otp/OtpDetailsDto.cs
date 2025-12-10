using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth.Application.Models.Dto.Otp
{
    public class OtpDetailsDto
    {
        public int Id { get; set; }
        public string CreateByUserId { get; set; }
        public string HashedOtp { get; set; }
        public string AccessKey { get; set; }
        public string CreateByClientIp { get; set; } 
        public bool IsUsed { get; set; }
        public int AttemptCount { get; set; } 
        public DateTime ExpiresAt { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsExpired { get; set; }
        public bool IsValid { get; set; }
    }
}
