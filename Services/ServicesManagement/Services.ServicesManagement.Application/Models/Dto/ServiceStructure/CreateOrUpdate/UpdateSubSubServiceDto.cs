using Services.ServicesManagement.Domain.Entities.ServiceStructure;
using System.ComponentModel.DataAnnotations;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceStructure
{
    public class UpdateSubSubServiceDto
    {
        public string Id { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        [Required]
        public string SubServiceId { get; set; }
    }
}
