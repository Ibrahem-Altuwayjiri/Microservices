using Services.Email.Application.Models.Dto.Template;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Models.Dto.TemplateDetails
{
    public class TemplateDetailsDto
    {
 
        public int VersionNumber { get; set; } 
        byte[]? HeaderImg { get; set; }
        byte[]? SubHeaderImg { get; set; }
        public string? TitleColor { get; set; }
        public string? FirstLineColor { get; set; }
        public string? SecondLineColor { get; set; }
        public string? ThirdLineColor { get; set; }
        public string? FooterColor { get; set; }
        byte[]? SubFooterImg { get; set; }
        byte[]? FooterImg { get; set; }
    }
}
