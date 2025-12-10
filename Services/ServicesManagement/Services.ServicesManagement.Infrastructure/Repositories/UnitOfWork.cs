using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.ServicesManagement.Domain.DBContext;
using Services.ServicesManagement.Domain.Entities.Base;
using Services.ServicesManagement.Domain.Entities.Lookups;
using Services.ServicesManagement.Domain.Entities.ServiceInfo;
using Services.ServicesManagement.Domain.Entities.ServiceStructure;
using Services.ServicesManagement.Domain.IRepositories;
using Services.ServicesManagement.Infrastructure.Helper;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Services.ServicesManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ServicesManagementDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IGenericRepository<ServiceDetails> ServiceDetailsRepository { get; set; }
        public IGenericRepository<Activities> ActivitiesRepository { get; set; }
        public IGenericRepository<DocumentName> DocumentNameRepository { get; set; }
        public IGenericRepository<DocumentValue> DocumentValueRepository { get; set; }
        public IGenericRepository<Domains> DomainsRepository { get; set; }
        public IGenericRepository<Header> HeaderRepository { get; set; }
        public IGenericRepository<HeaderValue> HeaderValueRepository { get; set; }
        public IGenericRepository<MainService> MainServiceRepository { get; set; }
        public IGenericRepository<ServiceActivities> ServiceActivitiesRepository { get; set; }
        public IGenericRepository<ServiceDomains> ServiceDomainsRepository { get; set; }
        public IGenericRepository<ServiceTags> ServiceTagsRepository { get; set; }
        public IGenericRepository<SubService> SubServiceRepository { get; set; }
        public IGenericRepository<SubSubService> SubSubServiceRepository { get; set; }
        public IGenericRepository<Tags> TagsRepository { get; set; }

        public UnitOfWork(ServicesManagementDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor;

            ServiceDetailsRepository = new GenericRepository<ServiceDetails>(_context);
            ActivitiesRepository = new GenericRepository<Activities>(_context);
            DocumentNameRepository = new GenericRepository<DocumentName>(_context);
            DocumentValueRepository = new GenericRepository<DocumentValue>(_context);
            DomainsRepository = new GenericRepository<Domains>(_context);
            HeaderRepository = new GenericRepository<Header>(_context);
            HeaderValueRepository = new GenericRepository<HeaderValue>(_context);
            MainServiceRepository = new GenericRepository<MainService>(_context);
            ServiceActivitiesRepository = new GenericRepository<ServiceActivities>(_context);
            ServiceDomainsRepository = new GenericRepository<ServiceDomains>(_context);
            ServiceTagsRepository = new GenericRepository<ServiceTags>(_context);
            SubServiceRepository = new GenericRepository<SubService>(_context);
            SubSubServiceRepository = new GenericRepository<SubSubService>(_context);
            TagsRepository = new GenericRepository<Tags>(_context);
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
            _context?.Dispose();
        }
    }
}
