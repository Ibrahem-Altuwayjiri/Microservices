using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Activities;
using Services.ServicesManagement.Application.Models.Dto.DocumentName;
using Services.ServicesManagement.Application.Models.Dto.Domains;
using Services.ServicesManagement.Application.Models.Dto.Header;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using Services.ServicesManagement.Application.Models.Dto.Tags;
using Services.ServicesManagement.Domain.Entities.ServiceStructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceInfo.CreateOrUpdate
{
    public class UpdateServiceDetailsDto 
    {
        public string Id { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        [Required]
        public string DescriptionAr { get; set; }
        [Required]
        public string DescriptionEn { get; set; }
        [Required]
        public string MainServiceId { get; set; }
        public string? SubServiceId { get; set; }
        public string? SubSubServiceId { get; set; }

        public List<int>? Activities { get; set; }
        public List<int>? Tags { get; set; }
        public List<int>? Domains { get; set; }
        public List<CreateOrUpdateDocumentValueDto>? DocumentValue { get; set; }
        public List<CreateOrUpdateHeaderValueDto>? HeaderValue { get; set; }
    }
}
