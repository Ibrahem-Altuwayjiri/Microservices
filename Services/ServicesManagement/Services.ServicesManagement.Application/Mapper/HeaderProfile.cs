using AutoMapper;
using Services.ServicesManagement.Application.Models.Dto.Header;
using Services.ServicesManagement.Domain.Entities.Lookups;

namespace Services.ServicesManagement.Application.Mapper
{
    public class HeaderProfile : Profile
    {
        public HeaderProfile()
        {
            CreateMap<Header, HeaderDto>().ReverseMap();
            CreateMap<Header, HeaderWithAuditDto>().ReverseMap();
            CreateMap<Header, CreateOrUpdateHeaderDto>().ReverseMap();
        }
    }
}
