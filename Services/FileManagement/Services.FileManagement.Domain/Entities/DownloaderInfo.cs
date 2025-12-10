using Services.FileManagement.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Domain.Entities
{
    public class DownloaderInfo : ICreateEntity
    {
        public int Id { get; set; }
        public string CreateByUserId { get; set; }
        public string CreateByClientIp { get; set; }
        public DateTime CreateDate { get; set; }
        [ForeignKey("MediaFile")]
        public string MediaFileId { get; set; }
        public MediaFile MediaFile { get; set; }
    }
}
