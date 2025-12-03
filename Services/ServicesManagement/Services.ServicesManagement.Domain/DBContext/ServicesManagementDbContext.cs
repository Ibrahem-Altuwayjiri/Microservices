using Microsoft.EntityFrameworkCore;
using Services.ServicesManagement.Domain.Entities.Lookups;
using Services.ServicesManagement.Domain.Entities.ServiceInfo;
using Services.ServicesManagement.Domain.Entities.ServiceStructure;
using System.Collections.Generic;
using Z.EntityFramework.Plus;


namespace Services.ServicesManagement.Domain.DBContext
{
    public class ServicesManagementDbContext : DbContext
    {
        public ServicesManagementDbContext(DbContextOptions<ServicesManagementDbContext> options) : base(options) { }

        // DbSets for domain entities

        // Service structure
        public DbSet<MainService> MainServices { get; set; }
        public DbSet<SubService> SubServices { get; set; }
        public DbSet<SubSubService> SubSubServices { get; set; }

        // Service details and relations
        public DbSet<ServiceDetails> ServiceDetails { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<HeaderValue> HeaderValues { get; set; }
        public DbSet<DocumentName> DocumentNames { get; set; }
        public DbSet<DocumentValue> DocumentValues { get; set; }
        public DbSet<ServiceActivities> ServiceActivities { get; set; }
        public DbSet<ServiceTags> ServiceTags { get; set; }
        public DbSet<ServiceDomains> ServiceDomains { get; set; }

        // Lookup entities
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Domains> Domains { get; set; }
        public DbSet<Activities> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<ServiceActivities>()
                .HasKey(t => new { t.ActivityId, t.ServiceDetailsId }); // Composite Key

            modelBuilder.Entity<ServiceTags>()
                .HasKey(t => new { t.TagId, t.ServiceDetailsId }); // Composite Key

            modelBuilder.Entity<ServiceDomains>()
                .HasKey(t => new { t.DomainId, t.ServiceDetailsId }); // Composite Key


            // Register AutoHistory using custom CLR type so history entries include UserId and ClientIp
            modelBuilder.EnableAutoHistory<CustomAutoHistory>(options => { });

            base.OnModelCreating(modelBuilder);
        }

    }
}
