using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Infrastructure.Utilities.EmailUtil
{
    public class EmailAttachment
    {
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; } = "application/octet-stream"; // default MIME
    }
}
