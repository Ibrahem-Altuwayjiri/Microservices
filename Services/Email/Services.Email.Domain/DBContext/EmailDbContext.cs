using Microsoft.EntityFrameworkCore;
using Services.Email.Domain.Entities;


namespace Services.Email.Domain.DBContext
{
    public class EmailDbContext : DbContext
    {
        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options) {  }


        public DbSet<Template> Template { get; set; }
        public DbSet<TemplateDetails> TemplateDetails { get; set; }
        public DbSet<RecipientType> RecipientType { get; set; }
        public DbSet<SenderInfo> SenderInfo { get; set; }
        public DbSet<EmailContent> EmailContent { get; set; }
        public DbSet<EmailDetails> EmailDetails { get; set; }
        public DbSet<Attachments> Attachments { get; set; }
        public DbSet<EmailRecipient> EmailRecipient { get; set; }
        public DbSet<EmailErrorLog> EmailErrorLog { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TemplateDetails>()
                .HasKey(t => new { t.TemplateId, t.VersionNumber }); // Composite Key

            // Register AutoHistory using custom CLR type so history entries include UserId and ClientIp
            modelBuilder.EnableAutoHistory<CustomAutoHistory>(options => { });

            base.OnModelCreating(modelBuilder);
        }
    }
}
