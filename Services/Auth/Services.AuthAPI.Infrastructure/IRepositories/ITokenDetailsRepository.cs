using Services.AuthAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Infrastructure.IRepositories
{
    public interface ITokenDetailsRepository : IGenericRepository<TokenDetails> { }
}
