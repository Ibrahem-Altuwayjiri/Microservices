using AutoMapper;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using Services.ServicesManagement.Domain.Entities.ServiceStructure;

namespace Services.ServicesManagement.Application.Mapper
{
    public class SubSubServiceProfile : Profile
    {
        public SubSubServiceProfile()
        {
            CreateMap<SubSubService, SubSubServiceDto>().ReverseMap();
            CreateMap<SubSubService, SubSubServiceWithAuditDto>().ReverseMap();
            CreateMap<SubSubService, CreateSubSubServiceDto>().ReverseMap();
            CreateMap<SubSubService, UpdateSubSubServiceDto>().ReverseMap();
        }
    }
}
