using Services.ServicesManagement.Application.Models.Dto.ServiceInfo;

namespace Services.ServicesManagement.Application.Models.Dto.DocumentName
{
    public class DocumentNameDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }

    }
}
