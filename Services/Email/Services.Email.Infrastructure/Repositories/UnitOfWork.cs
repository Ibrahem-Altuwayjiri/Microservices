
using Services.Email.Domain.DBContext;
using Services.Email.Domain.IRepositories;
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

        public IAttachmentsRepository AttachmentsRepository { get; set; }
        public IEmailContentRepository EmailContentRepository { get; set; }
        public IEmailDetailsRepository EmailDetailsRepository { get; set; }
        public IEmailErrorLogRepository EmailErrorLogRepository { get; set; }
        public IEmailRecipientRepository EmailRecipientRepository { get; set; }
        public IRecipientTypeRepository RecipientTypeRepository { get; set; }
        public ISenderInfoRepository SenderInfoRepository { get; set; }
        public ITemplateDetailsRepository TemplateDetailsRepository { get; set; }
        public ITemplateRepository TemplateRepository { get; set; }

        public UnitOfWork(EmailDbContext context)
        {
            _context = context;
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
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
