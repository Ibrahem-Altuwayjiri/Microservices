

using Services.ServicesManagement.Application.Models.Dto.Header;
using Services.ServicesManagement.Domain.Entities.ServiceInfo;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceInfo
{
    public class HeaderValueDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int HeaderId { get; set; }
        public HeaderDto Header { get; set; }
        public string ServiceDetailsId { get; set; }
    }
}
