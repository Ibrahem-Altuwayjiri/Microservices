using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Application.Models.Dto
{
    public class FileIds
    {
        [Required]
        public List<string> Id { get; set; }
    }
}
