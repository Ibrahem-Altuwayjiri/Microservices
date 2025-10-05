using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Infrastructure.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUsersRepository ApplicationUsersRepository { get; set; }
        ITokenDetailsRepository TokenDetailsRepository { get; set; }
        IOtpDetailsRepository OtpDetailsRepository { get; set; }

        Task<int> CompletedAsync();
    }
}
