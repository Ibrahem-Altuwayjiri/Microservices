using Services.Email.Application.Models.Dto.TemplateDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Models.Dto.Template
{
    public class   CreateTemplateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public CreateTemplateDetailsDto TemplateDetails { get; set; }
    }
}
