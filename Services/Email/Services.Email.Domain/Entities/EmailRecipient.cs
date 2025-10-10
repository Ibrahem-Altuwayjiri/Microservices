using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Domain.Entities
{
    public class EmailRecipient
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [ForeignKey("RecipientType")]
        public int RecipientTypeId { get; set; }
        public RecipientType RecipientType { get; set; }
        [ForeignKey("EmailDetails")]
        public int EmailDetailsId { get; set; }
        public EmailDetails EmailDetails { get; set; }
    }
}
