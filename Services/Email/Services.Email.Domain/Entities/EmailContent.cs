using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Domain.Entities
{
    public class EmailContent
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string? Title { get; set; }
        public string? FirstLine { get; set; }
        public string? SecondLine { get; set; }
        public string? ThirdLine { get; set; }
        public string? Footer { get; set; }
    }
}
