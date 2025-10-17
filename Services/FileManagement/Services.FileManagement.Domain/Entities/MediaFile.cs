using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Domain.Entities
{
    public class MediaFile
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? ServiceKey { get; set; } //TODO:
        [ForeignKey("UploaderInfo")]
        public int UploaderInfoId { get; set; }
        public UploaderInfo UploaderInfo { get; set; }
        [ForeignKey("FileDetails")]
        public int FileDetailsId { get; set; }
        public FileDetails FileDetails { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public string Extension { get; set; }
        public string MimeType { get; set; }

    }
}
