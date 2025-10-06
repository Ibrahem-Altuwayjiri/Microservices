using Services.Auth.Domain.Data;
using Services.Auth.Domain.Entities;
using Services.Auth.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth.Infrastructure.Repositories
{
    public class OtpDetailsRepository : GenericRepository<OtpDetails>, IOtpDetailsRepository
    {
        public OtpDetailsRepository(AppDbContext context) : base(context) { }
    }
}
