using AutoMapper;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure;
using Services.ServicesManagement.Application.Models.Dto.ServiceStructure.CreateOrUpdate;
using Services.ServicesManagement.Domain.Entities.ServiceStructure;

namespace Services.ServicesManagement.Application.Mapper
{
    public class MainServiceProfile : Profile
    {
        public MainServiceProfile()
        {
            CreateMap<MainService, MainServiceDto>().ReverseMap();
            CreateMap<MainService, MainServiceWithAuditDto>().ReverseMap();
            CreateMap<MainService, CreateMainServiceDto>().ReverseMap();
            CreateMap<MainService, UpdateMainServiceDto>().ReverseMap();
        }
    }
}
