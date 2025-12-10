using Services.ServicesManagement.Domain.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Domain.Entities.ServiceInfo
{
    public class DocumentValue
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string ReferenceId { get; set; }

        [ForeignKey("DocumentName")]
        public int DocumentNameId { get; set; }
        public DocumentName DocumentName { get; set; }
        [ForeignKey("ServiceDetails")]
        public string ServiceDetailsId { get; set; }
        public ServiceDetails ServiceDetails { get; set; }
    }
}
