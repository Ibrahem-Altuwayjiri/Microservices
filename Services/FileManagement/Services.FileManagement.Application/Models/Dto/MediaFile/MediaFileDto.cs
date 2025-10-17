using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Application.Models.Dto.MediaFile
{
    public class MediaFileDto
    {
        public string Id { get; set; }
        public string? ServiceKey { get; set; } //TODO:
        public string FileName { get; set; }

    }
}
