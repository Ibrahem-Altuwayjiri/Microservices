using Services.Auth.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth.Domain.Entities
{
    public class OtpDetails : ICreateEntity, IUpdateEntity
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string CreateByUserId { get; set; }
        public ApplicationUser User { get; set; }
        public string CreateByClientIp { get; set; }
        public DateTime CreateDate { get; set; }
        public string? UpdateByUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateByClientIp { get; set; }
        public string HashedOtp { get; set; }
        public string AccessKey { get; set; }
         
        public bool IsUsed { get; set; } = false;
        public int AttemptCount { get; set; } = 0;
        public DateTime ExpiresAt { get; set; }
        [NotMapped]
        public bool IsExpired => DateTime.Now <= ExpiresAt;
        public bool IsValid { get; set; } = true;
    }
}
