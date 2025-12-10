using Services.ServicesManagement.Application.Models.Abstract;

namespace Services.ServicesManagement.Application.Models.Dto.Tags
{
    public class TagsWithAuditDto : ICreateEntityDto, IUpdateEntityDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public string CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateByClientIp { get; set; }
        public string? UpdateByUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateByClientIp { get; set; }
    }
}
