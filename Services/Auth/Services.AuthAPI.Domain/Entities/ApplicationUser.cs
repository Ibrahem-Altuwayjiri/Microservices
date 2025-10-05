using Microsoft.AspNetCore.Identity;

namespace Services.AuthAPI.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime? CraeteDate { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime? LastLogin { get; set; } 
    }
}
