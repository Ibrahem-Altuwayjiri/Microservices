using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Domain.Entities
{
    public class Attachments
    {
        public int Id { get; set; }
        public string ReferenceId { get; set; }
        public string Name { get; set; }
        public string Extensions { get; set; }
        public string? Description { get; set; }
        public string Path { get; set; }
        [ForeignKey("EmailDetails")]
        public int EmailDetailsId { get; set; }
        public EmailDetails EmailDetails { get; set; }
    }
}
