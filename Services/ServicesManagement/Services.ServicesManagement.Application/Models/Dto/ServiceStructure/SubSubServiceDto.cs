using Services.ServicesManagement.Domain.Entities.ServiceStructure;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceStructure
{
    public class SubSubServiceDto
    {
        public string Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public string SubServiceId { get; set; }
        public SubServiceDto SubService { get; set; }
    }
}
