using Services.ServicesManagement.Application.Models.Dto.ServiceInfo;
using System.ComponentModel.DataAnnotations;

namespace Services.ServicesManagement.Application.Models.Dto.DocumentName
{
    public class CreateOrUpdateDocumentNameDto
    {
        public int Id { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }

    }
}
