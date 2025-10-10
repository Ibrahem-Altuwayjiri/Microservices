using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Domain.Entities
{
    public class EmailDetails
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now.ToLocalTime();    
        public DateTime? ScheduleDate { get; set; }
        [ForeignKey("SenderInfo")]
        public int SenderInfoId { get; set; }
        public SenderInfo SenderInfo { get; set; }
        [ForeignKey("Template")]
        public int TemplateId { get; set; }
        public Template Template { get; set; }
        [ForeignKey("EmailContent")]
        public int EmailContentId { get; set; }
        public EmailContent EmailContent { get; set; }
        public bool IsSend { get; set; } = false;
        public DateTime? SendDate {  get; set; }
        public int TryNum { get; set; } = 0;
        public DateTime? LastTrySend {  get; set; }

        public List<EmailRecipient> EmailRecipients { get; set; }
        public List<Attachments> Attachments { get; set; }
    }
}
