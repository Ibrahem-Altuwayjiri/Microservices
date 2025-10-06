using Services.AuthAPI.Domain.Data;
using Services.AuthAPI.Domain.Entities;
using Services.AuthAPI.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Infrastructure.Repositories
{
    public class TokenDetailsRepository : GenericRepository<TokenDetails>, ITokenDetailsRepository
    {
        public TokenDetailsRepository(AppDbContext context) : base(context) { }
    }
}
