using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Shared.Models.Dto.Otp
{
    public class OtpResponseDto
    {
        public string AccessKey { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}

