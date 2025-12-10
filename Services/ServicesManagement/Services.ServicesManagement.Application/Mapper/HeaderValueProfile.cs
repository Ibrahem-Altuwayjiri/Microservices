using AutoMapper;
using Services.ServicesManagement.Application.Models.Dto.Header;
using Services.ServicesManagement.Application.Models.Dto.ServiceInfo;
using Services.ServicesManagement.Domain.Entities.Lookups;
using Services.ServicesManagement.Domain.Entities.ServiceInfo;

namespace Services.ServicesManagement.Application.Mapper
{
    public class HeaderValueProfile : Profile
    {
        public HeaderValueProfile()
        {
            CreateMap<HeaderValue, HeaderValueDto>().ReverseMap();

        }
    }
}
