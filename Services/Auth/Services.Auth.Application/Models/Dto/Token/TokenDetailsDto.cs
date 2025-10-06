using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth.Application.Models.Dto.Token
{
    public class TokenDetailsDto
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateByDeviceIP { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpires { get; set; }
        public bool IsTokenExpired { get; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
        public bool IsRefreshTokenExpired { get; }
        public DateTime LastUpdateDate { get; set; }
        public int NumberOfUpdate { get; set; }
        public DateTime? RevokedDate { get; set; }
        public bool IsActive { get; }
    }
}
