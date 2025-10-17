using Services.Email.Application.Models.Dto.EmailDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Models.Dto.EmailErrorLog
{
    public class EmailErrorLogDto
    {
        public int Id { get; set; }
        public int EmailDetailsId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
