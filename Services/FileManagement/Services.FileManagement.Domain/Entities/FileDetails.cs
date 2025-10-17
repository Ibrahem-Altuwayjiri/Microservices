

namespace Services.FileManagement.Domain.Entities
{
    public class FileDetails
    {
        public int Id { get; set; }
        public byte[]? FileBytes { get; set; }
        public string ExternalStorageReferenceId { get; set; } // use as Path if stored locally in the server or reference id if stored in external storage;
        public bool IsUpload { get; set; } = false;
        public DateTime? UploadDate { get; set; }
        public int TryNum { get; set; } = 0;
        public DateTime? LastTrySend { get; set; }
    }
}
