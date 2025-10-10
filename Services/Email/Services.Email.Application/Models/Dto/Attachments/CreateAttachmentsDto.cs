using Services.Email.Application.Models.Dto.EmailDetails;
using Services.Email.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Models.Dto.Attachments
{
    public class CreateAttachmentsDto
    {
        [Required]
        public string ReferenceId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Extensions { get; set; }
        public string? Description { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public int EmailDetailsId { get; set; }
    }
}
