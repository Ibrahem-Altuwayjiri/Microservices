using Services.FileManagement.Domain.DBContext;
using Services.FileManagement.Domain.Entities;
using Services.FileManagement.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Infrastructure.Repositories
{
    public class MediaFileRepository : GenericRepository<MediaFile>, IMediaFileRepository
    {
        public MediaFileRepository(FileManagementDbContext context) : base(context) { }
    }
}
