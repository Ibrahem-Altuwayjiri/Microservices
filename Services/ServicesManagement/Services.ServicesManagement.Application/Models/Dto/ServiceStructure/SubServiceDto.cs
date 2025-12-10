

using Services.ServicesManagement.Domain.Entities.ServiceStructure;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceStructure
{
    public class SubServiceDto
    {
        public string Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public string MainServiceId { get; set; }
        public MainServiceDto MainService { get; set; }
        //public List<SubSubServiceDto> SubSubServices { get; set; }
    }
}
