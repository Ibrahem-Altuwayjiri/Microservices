using Services.ServicesManagement.Domain.Entities.ServiceStructure;
using System.ComponentModel.DataAnnotations;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceStructure.CreateOrUpdate
{
    public class CreateMainServiceDto
    {
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
    }
}
