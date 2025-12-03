using Services.ServicesManagement.Domain.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Domain.Entities.ServiceInfo
{
    public class ServiceTags
    {
        [ForeignKey("ServiceDetails")]
        public string ServiceDetailsId { get; set; }
        public ServiceDetails ServiceDetails { get; set; }
        [ForeignKey("Tags")]
        public int TagId { get; set; }
        public Tags Tags { get; set; }
    }
}
