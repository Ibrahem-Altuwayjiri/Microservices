using Services.ServicesManagement.Application.Models.Abstract;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceStructure
{
    public class SubServiceWithAuditDto : ICreateEntityDto, IUpdateEntityDto
    {
        public string Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public string MainServiceId { get; set; }
        public MainServiceWithAuditDto MainService { get; set; }
        //public List<SubSubServiceWithAuditDto> SubSubServices { get; set; }
        public string CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateByClientIp { get; set; }
        public string? UpdateByUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateByClientIp { get; set; }
    }
}
