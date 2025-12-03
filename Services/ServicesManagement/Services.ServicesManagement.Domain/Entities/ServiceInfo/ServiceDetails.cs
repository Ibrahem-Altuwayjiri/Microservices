using Services.ServicesManagement.Domain.Entities.Base;
using Services.ServicesManagement.Domain.Entities.ServiceStructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Domain.Entities.ServiceInfo
{
    public class ServiceDetails : ICreateEntity, IUpdateEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey("MainService")]
        public string MainServiceId { get; set; }
        public MainService MainService { get; set; }
        [ForeignKey("SubService")]
        public string SubServiceId { get; set; }
        public SubService SubService { get; set; }

        [ForeignKey("SubSubService")]
        public string SubSubServiceId { get; set; }
        public SubSubService SubSubService { get; set; }

        public string CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateByClientIp { get; set; }
        public string? UpdateByUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateByClientIp { get; set; }

        public List<ServiceActivities> ServiceActivities { get; set; }
        public List<ServiceTags> ServiceTags { get; set; }
        public List<ServiceDomains> ServiceDomains { get; set; }
        public List<DocumentValue> DocumentValue { get; set; }
        public List<HeaderValue> HeaderValue { get; set; }

    }
}
