using Services.ServicesManagement.Domain.Entities.ServiceStructure;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceStructure
{
    public class MainServiceDto
    {
        public string Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        //public List<SubServiceDto> SubServices { get; set; }
    }
}
