using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Domain.DBContext
{
    public class CustomAutoHistory : AutoHistory
    {
        public string UserId { get; set; }
        public string ClientIp { get; set; }
    }
}
