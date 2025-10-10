using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Models.Dto.Attachments
{
    public class SendAttachmentsDto
    {
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
    }
}
