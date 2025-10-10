using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Models.Dto.SenderInfo
{
    public class SenderInfoDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClientIp { get; set; }
    }
}
