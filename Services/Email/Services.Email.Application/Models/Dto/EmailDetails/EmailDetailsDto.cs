using Services.Email.Application.Models.Dto.Attachments;
using Services.Email.Application.Models.Dto.EmailContent;
using Services.Email.Application.Models.Dto.EmailRecipient;
using Services.Email.Application.Models.Dto.SenderInfo;
using Services.Email.Application.Models.Dto.Template;
using Services.Email.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Models.Dto.EmailDetails
{
    public class EmailDetailsDto
    {
        public int Id { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public SenderInfoDto SenderInfo { get; set; }
        public TemplateDto Template { get; set; }
        public EmailContentDto EmailContent { get; set; }

        public List<EmailRecipientDto> EmailRecipients { get; set; }
        public List<AttachmentsDto> Attachments { get; set; }
    }
}
