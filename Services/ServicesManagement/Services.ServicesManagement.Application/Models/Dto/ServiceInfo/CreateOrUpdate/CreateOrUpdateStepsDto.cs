
using System.ComponentModel.DataAnnotations;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceInfo
{
    public class CreateOrUpdateStepsDto
    {
        public int Id { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
    }
}
