using AutoMapper;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using Services.ServicesManagement.Domain.Entities.ServiceStructure;

namespace Services.ServicesManagement.Application.Mapper
{
    public class SubServiceProfile : Profile
    {
        public SubServiceProfile()
        {
            CreateMap<SubService, SubServiceDto>().ReverseMap();
            CreateMap<SubService, SubServiceWithAuditDto>().ReverseMap();
            CreateMap<SubService, CreateSubServiceDto>().ReverseMap();
            CreateMap<SubService, UpdateSubServiceDto>().ReverseMap();
        }
    }
}
