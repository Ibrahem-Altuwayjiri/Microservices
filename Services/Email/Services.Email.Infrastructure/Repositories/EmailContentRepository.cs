using Services.Email.Domain.DBContext;
using Services.Email.Domain.Entities;
using Services.Email.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Infrastructure.Repositories
{
    public class EmailContentRepository : GenericRepository<EmailContent>, IEmailContentRepository
    {
        public EmailContentRepository(EmailDbContext context) : base(context) { }
    }
}
