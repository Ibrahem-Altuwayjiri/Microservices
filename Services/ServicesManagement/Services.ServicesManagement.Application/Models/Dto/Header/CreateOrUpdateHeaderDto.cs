using Services.ServicesManagement.Application.Models.Dto.ServiceInfo;
using System.ComponentModel.DataAnnotations;

namespace Services.ServicesManagement.Application.Models.Dto.Header
{
    public class CreateOrUpdateHeaderDto
    {
        public int Id { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }

    }
}
