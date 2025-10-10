using Services.Email.Application.Models.Dto.EmailDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Models.Dto.EmailErrorLog
{
    public class CreateEmailErrorLogDto
    {
        [Required]
        public int EmailDetailsId { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
