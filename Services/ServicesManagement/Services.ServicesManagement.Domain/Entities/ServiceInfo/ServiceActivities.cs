using Services.ServicesManagement.Domain.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Domain.Entities.ServiceInfo
{
    public class ServiceActivities
    {
        [ForeignKey("ServiceDetails")]
        public string ServiceDetailsId { get; set; }
        public ServiceDetails ServiceDetails { get; set; }
        [ForeignKey("Activities")]
        public int ActivityId { get; set; }
        public Activities Activities { get; set; }

    }
}
