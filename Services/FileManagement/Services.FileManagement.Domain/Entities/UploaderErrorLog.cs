using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Domain.Entities
{
    public class UploaderErrorLog
    {
        public int Id { get; set; }
        [ForeignKey("MediaFile")]
        public string MediaFileId { get; set; }
        public MediaFile MediaFile { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.ToLocalTime();
        public string Message { get; set; }
    }
}
