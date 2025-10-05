using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Shared.Models.Dto.User
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; } 
        public DateTime? CraeteDate { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
