using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Services.AuthAPI.Domain.Entities;


namespace Services.AuthAPI.Domain.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<TokenDetails> TokenDetails { get; set; }
        public DbSet<OtpDetails> OtpDetails { get; set; }

       
    }
}
