using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Auth.Domain.Data;
using Services.Auth.Domain.IRepositories;
using Services.Auth.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public IApplicationUsersRepository ApplicationUsersRepository { get; set; }
        public ITokenDetailsRepository TokenDetailsRepository { get; set; }
        public IOtpDetailsRepository OtpDetailsRepository { get; set; }

        public UnitOfWork(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

            ApplicationUsersRepository = new ApplicationUsersRepository(_context);
            TokenDetailsRepository = new TokenDetailsRepository(_context);
            OtpDetailsRepository = new OtpDetailsRepository(_context);
            
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
