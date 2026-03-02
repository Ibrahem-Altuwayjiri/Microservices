namespace Services.Frontend.Web.Services
{
    public interface IFileManagementService
    {
        Task<bool> UploadFileAsync(IFormFile file, string purpose);
        Task<FileDto> DownloadFileAsync(int fileId);
        Task<List<FileDto>> GetFilesAsync();
    }

    public class FileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}
