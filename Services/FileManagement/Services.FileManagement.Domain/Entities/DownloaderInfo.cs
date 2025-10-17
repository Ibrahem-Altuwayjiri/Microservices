using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Domain.Entities
{
    public class DownloaderInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClientIp { get; set; }
        [ForeignKey("MediaFile")]
        public string MediaFileId { get; set; }
        public MediaFile MediaFile { get; set; }
        public DateTime DownloadDate { get; set; } = DateTime.UtcNow.ToLocalTime();
    }
}
