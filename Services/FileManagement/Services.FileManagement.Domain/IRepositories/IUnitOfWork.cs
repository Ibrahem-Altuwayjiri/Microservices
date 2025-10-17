

namespace Services.FileManagement.Domain.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IMediaFileRepository MediaFileRepository { get; set; }
        IUploaderInfoRepository UploaderInfoRepository { get; set; }
        IUploaderErrorLogRepository UploaderErrorLogRepository { get; set; }
        IFileDetailsRepository TempFileInfoRepository { get; set; }
        IDownloaderInfoRepository DownloaderInfoRepository { get; set; }


        Task<int> CompletedAsync();
    }
}
