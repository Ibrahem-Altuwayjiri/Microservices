using Services.ServicesManagement.Application.Models.Abstract;
using Services.ServicesManagement.Domain.Entities.ServiceStructure;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceStructure
{
    public class SubSubServiceWithAuditDto : ICreateEntityDto, IUpdateEntityDto
    {
        public string Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public string SubServiceId { get; set; }
        public SubServiceWithAuditDto SubService { get; set; }
        public string CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateByClientIp { get; set; }
        public string? UpdateByUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateByClientIp { get; set; }
    }
}
