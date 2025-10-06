using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Application.Models.Dto.Token
{
    public class JWTTokenDto
    {
        public string Token { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
