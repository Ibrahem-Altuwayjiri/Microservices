using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth.Domain.Entities.Base
{
    public interface ICreateEntity
    {
        public string CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; } 
        public string CreateByClientIp { get; set; }
    }
}
