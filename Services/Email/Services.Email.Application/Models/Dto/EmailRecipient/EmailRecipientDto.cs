using Services.Email.Application.Models.Dto.EmailDetails;
using Services.Email.Application.Models.Dto.RecipientType;
using Services.Email.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Models.Dto.EmailRecipient
{
    public class EmailRecipientDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int RecipientTypeId { get; set; }

    }
}
