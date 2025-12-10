using Microsoft.AspNetCore.Identity;
using Services.Auth.Domain.Entities.Base;

namespace Services.Auth.Domain.Entities
{
    public class ApplicationUser : IdentityUser, ICreateEntity, IUpdateEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateByClientIp { get; set; }
        public string? UpdateByUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateByClientIp { get; set; }
        public DateTime? LastLogin { get; set; } 
    }
}
