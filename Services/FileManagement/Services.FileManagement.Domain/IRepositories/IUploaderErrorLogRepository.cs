using Services.FileManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Domain.IRepositories
{
    public interface IUploaderErrorLogRepository : IGenericRepository<UploaderErrorLog>
    {
    }
}
