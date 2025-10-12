using Services.Email.Application.Models.Dto.Template;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Models.Dto.TemplateDetails
{
    public class CreateTemplateDetailsDto
    {
        [Required]
        byte[] HeaderImg { get; set; }
        byte[]? SubHeaderImg { get; set; }
        [Required]
        public string TitleColor { get; set; }
        [Required]
        public string FirstLineColor { get; set; }
        [Required]
        public string SecondLineColor { get; set; }
        [Required]
        public string ThirdLineColor { get; set; }
        [Required]
        public string FooterColor { get; set; }
        byte[]? SubFooterImg { get; set; }
        [Required]
        byte[] FooterImg { get; set; }
    }
}
