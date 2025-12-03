

using Services.ServicesManagement.Application.Models.Dto.Header;
using Services.ServicesManagement.Domain.Entities.ServiceInfo;
using System.ComponentModel.DataAnnotations;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceInfo
{
    public class CreateOrUpdateHeaderValueDto
    {
        public int Id { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        [Required]
        public int HeaderId { get; set; }

    }
}
