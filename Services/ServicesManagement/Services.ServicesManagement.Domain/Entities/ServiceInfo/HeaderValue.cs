using Services.ServicesManagement.Domain.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Domain.Entities.ServiceInfo
{
    public class HeaderValue
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        [ForeignKey("Header")]
        public int HeaderId { get; set; }
        public Header Header { get; set; }
        [ForeignKey("ServiceDetails")]
        public string ServiceDetailsId { get; set; }
        public ServiceDetails ServiceDetails { get; set; }
    }
}
