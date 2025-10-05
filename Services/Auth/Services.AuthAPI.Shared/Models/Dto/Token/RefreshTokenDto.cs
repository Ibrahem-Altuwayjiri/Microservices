using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Shared.Models.Dto.Token
{
    public class RefreshTokenDto
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
    }
}
