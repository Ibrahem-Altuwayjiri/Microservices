using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Domain.Entities
{
    public class Template
    {
        public int Id { get; set; }
        public int VersionNumber { get; set; } = 0;
        public string Name { get; set; }
        public List<TemplateDetails> TemplateDetails { get; set; }
    }
}
