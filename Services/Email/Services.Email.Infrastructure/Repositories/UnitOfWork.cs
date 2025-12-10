
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Email.Domain.DBContext;
using Services.Email.Domain.IRepositories;
using Services.Email.Infrastructure.Helper;
using Services.Email.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EmailDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IAttachmentsRepository AttachmentsRepository { get; set; }
        public IEmailContentRepository EmailContentRepository { get; set; }
        public IEmailDetailsRepository EmailDetailsRepository { get; set; }
        public IEmailErrorLogRepository EmailErrorLogRepository { get; set; }
        public IEmailRecipientRepository EmailRecipientRepository { get; set; }
        public IRecipientTypeRepository RecipientTypeRepository { get; set; }
        public ISenderInfoRepository SenderInfoRepository { get; set; }
        public ITemplateDetailsRepository TemplateDetailsRepository { get; set; }
        public ITemplateRepository TemplateRepository { get; set; }

        public UnitOfWork(EmailDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

            AttachmentsRepository = new AttachmentsRepository(_context);
            EmailContentRepository = new EmailContentRepository(_context);
            EmailDetailsRepository = new EmailDetailsRepository(_context);
            EmailErrorLogRepository = new EmailErrorLogRepository(_context);
            EmailRecipientRepository = new EmailRecipientRepository(_context);
            SenderInfoRepository = new SenderInfoRepository(_context);
            TemplateDetailsRepository = new TemplateDetailsRepository(_context);
            TemplateRepository = new TemplateRepository(_context);
            RecipientTypeRepository = new RecipientTypeRepository(_context);
            
        }

        public async Task<int> CompletedAsync()
        {

            // Set audit fields based on entity state
            var now = DateTime.UtcNow;

            string userId = ClientInfoHelper.GetUserId(_httpContextAccessor.HttpContext);
            string clientIp = ClientInfoHelper.GetClientIp(_httpContextAccessor.HttpContext);




            var entries = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .ToList();

            foreach (var entry in entries)
            {
                // Handle Added
                if (entry.State == EntityState.Added)
                {
                    // If entity has CreateEntity properties, set them
                    var createByProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "CreateByUserId");
                    if (createByProp != null)
                        createByProp.CurrentValue = userId;

                    var createDateProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "CreateDate");
                    if (createDateProp != null)
                        createDateProp.CurrentValue = now;

                    var clientIpProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "CreateByClientIp");
                    if (clientIpProp != null)
                        clientIpProp.CurrentValue = clientIp;
                }

                // Handle Modified
                if (entry.State == EntityState.Modified)
                {
                    // Prevent modification of Create* properties
                    var createByProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "CreateByUserId");
                    if (createByProp != null)
                        createByProp.IsModified = false;

                    var createDateProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "CreateDate");
                    if (createDateProp != null)
                        createDateProp.IsModified = false;

                    var clientIpCreateProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "CreateByClientIp");
                    if (clientIpCreateProp != null)
                        clientIpCreateProp.IsModified = false;

                    // Set auditable fields
                    var updateByProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "UpdateByUserId");
                    if (updateByProp != null)
                        updateByProp.CurrentValue = userId;

                    var updateDateProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "UpdateDate");
                    if (updateDateProp != null)
                        updateDateProp.CurrentValue = now;

                    var clientIpUpdateProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "UpdateByClientIp");
                    if (clientIpUpdateProp != null)
                        clientIpUpdateProp.CurrentValue = clientIp;
                }
            }

            // EnsureAutoHistory factory to populate custom history fields
            _context.EnsureAutoHistory<CustomAutoHistory>(() => new CustomAutoHistory { UserId = userId, ClientIp = clientIp });

            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
