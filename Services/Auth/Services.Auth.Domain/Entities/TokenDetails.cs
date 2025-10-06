﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth.Domain.Entities
{
    public class TokenDetails
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateByDeviceIP { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpires { get; set; }
        [NotMapped]
        public bool IsTokenExpired => DateTime.Now >= TokenExpires;
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
        [NotMapped]
        public bool IsRefreshTokenExpired => DateTime.Now >= RefreshTokenExpires;
        public DateTime LastUpdateDate { get; set; }
        public int NumberOfUpdate { get; set; }
        public DateTime? RevokedDate { get; set; }
        [NotMapped]
        public bool IsActive  => RevokedDate == null && !IsRefreshTokenExpired;

    }
}
