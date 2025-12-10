
using Services.ServicesManagement.Application.Models.Dto.DocumentName;
using Services.ServicesManagement.Domain.Entities.ServiceInfo;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceInfo
{
    public class DocumentValueDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string ReferenceId { get; set; }
        public int DocumentNameId { get; set; }
        public DocumentNameDto DocumentName { get; set; }
        public string ServiceDetailsId { get; set; }
    }
}
