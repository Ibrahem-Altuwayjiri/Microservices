using AutoMapper;
using Services.ServicesManagement.Application.Models.Dto.Domains;
using Services.ServicesManagement.Domain.Entities.Lookups;

namespace Services.ServicesManagement.Application.Mapper
{
    public class DomainsProfile : Profile
    {
        public DomainsProfile()
        {
            CreateMap<Domains, DomainsDto>().ReverseMap();
            CreateMap<Domains, DomainsWithAuditDto>().ReverseMap();
            CreateMap<Domains, CreateOrUpdateDomainsDto>().ReverseMap();
        }
    }
}
