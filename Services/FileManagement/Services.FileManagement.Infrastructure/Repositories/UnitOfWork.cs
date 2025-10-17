
using Services.FileManagement.Domain.DBContext;
using Services.FileManagement.Domain.IRepositories;
using Services.FileManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FileManagementDbContext _context;

        public IMediaFileRepository MediaFileRepository { get; set; }
        public IUploaderInfoRepository UploaderInfoRepository { get; set; }
        public IUploaderErrorLogRepository UploaderErrorLogRepository { get; set; }
        public IFileDetailsRepository TempFileInfoRepository { get; set; }
        public IDownloaderInfoRepository DownloaderInfoRepository { get; set; }


        public UnitOfWork(FileManagementDbContext context)
        {
            _context = context;
            MediaFileRepository = new MediaFileRepository(_context);
            UploaderInfoRepository = new UploaderInfoRepository(_context);
            UploaderErrorLogRepository = new UploaderErrorLogRepository(_context);
            TempFileInfoRepository = new FileDetailsRepository(_context);
            DownloaderInfoRepository = new DownloaderInfoRepository(_context);



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
