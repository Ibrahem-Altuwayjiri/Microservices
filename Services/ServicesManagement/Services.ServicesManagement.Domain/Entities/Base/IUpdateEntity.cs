using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Domain.Entities.Base
{
    public interface IUpdateEntity
    {
        public string? UpdateByUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateByClientIp { get; set; }

    }
}
