using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Infrastructure.Utilities.EmailUtil
{
    public class SendEmailDetails
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public string[] ToEmails { get; set; }
        public string[] CcEmails { get; set; }
        public string[] BccEmails { get; set; }
        public List<EmailAttachment> Attachments { get; set; }
    }
}
