using Services.AuthAPI.Domain.Entities;
using Services.AuthAPI.Infrastructure.Data;
using Services.AuthAPI.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Application.Repositories
{
    public class TokenDetailsRepository : GenericRepository<TokenDetails>, ITokenDetailsRepository
    {
        public TokenDetailsRepository(AppDbContext context) : base(context) { }
    }
}
