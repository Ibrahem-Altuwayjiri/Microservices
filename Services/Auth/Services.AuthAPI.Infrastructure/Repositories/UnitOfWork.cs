using Services.AuthAPI.Domain.Data;
using Services.AuthAPI.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IApplicationUsersRepository ApplicationUsersRepository { get; set; }
        public ITokenDetailsRepository TokenDetailsRepository { get; set; }
        public IOtpDetailsRepository OtpDetailsRepository { get; set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            ApplicationUsersRepository = new ApplicationUsersRepository(_context);
            TokenDetailsRepository = new TokenDetailsRepository(_context);
            OtpDetailsRepository = new OtpDetailsRepository(_context);

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
