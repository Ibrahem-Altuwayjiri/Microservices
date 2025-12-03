using Services.ServicesManagement.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Domain.Entities.ServiceStructure
{
    public class SubSubService : ICreateEntity, IUpdateEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; } = true;
        [ForeignKey("SubService")]
        public string SubServiceId { get; set; }
        public SubService SubService { get; set; }
        public string CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateByClientIp { get; set; }
        public string? UpdateByUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateByClientIp { get; set; }
    }
}
