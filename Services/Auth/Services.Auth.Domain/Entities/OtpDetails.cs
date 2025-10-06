using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth.Domain.Entities
{
    public class OtpDetails
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string HashedOtp { get; set; }
        public string AccessKey { get; set; }
        public string RequestIp { get; set; } 
        public bool IsUsed { get; set; } = false;
        public int AttemptCount { get; set; } = 0;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [NotMapped]
        public bool IsExpired => DateTime.Now <= ExpiresAt;
        public bool IsValid { get; set; } = true;
    }
}
