using Services.ServicesManagement.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Domain.Entities.ServiceStructure
{
    public class MainService : ICreateEntity, IUpdateEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; } = true;
        //public List<SubService> SubServices { get; set; }

        public string CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateByClientIp { get; set; }
        public string? UpdateByUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateByClientIp { get; set; }
    }
}
