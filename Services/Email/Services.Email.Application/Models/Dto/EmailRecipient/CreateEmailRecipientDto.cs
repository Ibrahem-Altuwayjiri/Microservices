using Services.Email.Application.Models.Dto.EmailDetails;
using Services.Email.Application.Models.Dto.RecipientType;
using Services.Email.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Models.Dto.EmailRecipient
{
    public class CreateEmailRecipientDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int RecipientTypeId { get; set; }

    }
}
