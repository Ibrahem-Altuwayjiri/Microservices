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
    public class CreateEmailDetailsDto
    {
        public int TemplateId { get; set; }
        //public CreateSenderInfoDto SenderInfo { get; set; }
        public List<CreateEmailRecipientDto> EmailRecipients { get; set; }
        public CreateEmailContentDto EmailContent { get; set; }
        public List<CreateAttachmentsDto> Attachments { get; set; }
        public DateTime? ScheduleDate { get; set; } = DateTime.Now;

    }
}
