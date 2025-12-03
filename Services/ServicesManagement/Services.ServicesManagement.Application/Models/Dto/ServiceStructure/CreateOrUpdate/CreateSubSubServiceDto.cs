using Services.ServicesManagement.Domain.Entities.ServiceStructure;
using System.ComponentModel.DataAnnotations;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceStructure
{
    public class CreateSubSubServiceDto
    {
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        [Required]
        public string SubServiceId { get; set; }
    }
}
