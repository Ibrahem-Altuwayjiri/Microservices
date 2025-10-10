using Services.Email.Application.Models.Dto.TemplateDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Models.Dto.Template
{
    public class TemplateDto
    {
        public int Id { get; set; }
        public int VersionNumber { get; set; }
        public string Name { get; set; }
        public TemplateDetailsDto templateDetails { get; set; } // last version
    }
}
