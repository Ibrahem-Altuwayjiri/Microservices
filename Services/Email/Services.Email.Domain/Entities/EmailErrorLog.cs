using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Domain.Entities
{
    public class EmailErrorLog
    {
        public int Id { get; set; }
        [ForeignKey("EmailDetails")]
        public int EmailDetailsId { get; set; }
        public EmailDetails EmailDetails { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.ToLocalTime();
        public string Message { get; set; }
    }
}
