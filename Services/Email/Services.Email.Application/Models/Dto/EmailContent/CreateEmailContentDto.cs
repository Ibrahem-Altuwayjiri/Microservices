using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Models.Dto.EmailContent
{
    public class CreateEmailContentDto
    {
        [Required]
        public string Subject { get; set; }
        public string? Title { get; set; }
        public string? FirstLine { get; set; }
        public string? SecondLine { get; set; }
        public string? ThirdLine { get; set; }
        public string? Footer { get; set; }
    }
}
