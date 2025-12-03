
using Microsoft.AspNetCore.Http;
using Services.ServicesManagement.Application.Models.Dto.DocumentName;
using Services.ServicesManagement.Domain.Entities.ServiceInfo;
using System.ComponentModel.DataAnnotations;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceInfo
{
    public class CreateOrUpdateDocumentValueDto
    {
        public int Id { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public int DocumentNameId { get; set; }

    }
}
