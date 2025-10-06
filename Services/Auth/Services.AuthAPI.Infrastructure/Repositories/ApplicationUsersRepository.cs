using Services.AuthAPI.Domain.Data;
using Services.AuthAPI.Domain.Entities;
using Services.AuthAPI.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Infrastructure.Repositories
{
    public class ApplicationUsersRepository : GenericRepository<ApplicationUser>, IApplicationUsersRepository
    {
        public ApplicationUsersRepository(AppDbContext context) : base(context) { }
    }
}
