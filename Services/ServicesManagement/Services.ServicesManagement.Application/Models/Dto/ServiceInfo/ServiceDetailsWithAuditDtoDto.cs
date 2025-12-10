using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Application.Models.Dto.Activities;
using Services.ServicesManagement.Application.Models.Dto.DocumentName;
using Services.ServicesManagement.Application.Models.Dto.Domains;
using Services.ServicesManagement.Application.Models.Dto.Header;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using Services.ServicesManagement.Application.Models.Dto.Tags;
using Services.ServicesManagement.Domain.Entities.ServiceStructure;
using System.Collections.Generic;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceInfo
{
    public class ServiceDetailsWithAuditDto : ICreateEntityDto, IUpdateEntityDto
    {
        public string Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public bool IsActive { get; set; } = true;
        public string MainServiceId { get; set; }
        public MainServiceDto MainService { get; set; }
        public string SubServiceId { get; set; }
        public SubServiceDto SubService { get; set; }
        public string SubSubServiceId { get; set; }
        public SubSubServiceDto SubSubService { get; set; }

        public string CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateByClientIp { get; set; }
        public string? UpdateByUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateByClientIp { get; set; }

        // Related 
        public List<ActivitiesDto> Activities { get; set; }
        public List<TagsDto> Tags { get; set; }
        public List<DomainsDto> Domains { get; set; }
        public List<DocumentValueDto> DocumentValue { get; set; }
        public List<HeaderValueDto> HeaderValue { get; set; }
    }
}
