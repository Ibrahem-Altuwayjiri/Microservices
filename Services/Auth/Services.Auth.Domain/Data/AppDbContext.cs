using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Services.Auth.Domain.Entities;
using Services.Email.Domain.DBContext;


namespace Services.Auth.Domain.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<TokenDetails> TokenDetails { get; set; }
        public DbSet<OtpDetails> OtpDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // Register AutoHistory using custom CLR type so history entries include UserId and ClientIp
            modelBuilder.EnableAutoHistory<CustomAutoHistory>(options => { });

            base.OnModelCreating(modelBuilder);
        }

    }
}
