using AutoMapper;
using Services.ServicesManagement.Application.Models.Dto.ServiceInfo;
using Services.ServicesManagement.Application.Models.Dto.ServiceInfo.CreateOrUpdate;
using Services.ServicesManagement.Domain.Entities.ServiceInfo;

namespace Services.ServicesManagement.Application.Mapper
{
    public class ServiceInfoProfile : Profile
    {
        public ServiceInfoProfile()
        {
            CreateMap<ServiceActivities, ServiceActivitiesDto>().ReverseMap();
            CreateMap<ServiceTags, ServiceTagsDto>().ReverseMap();
            CreateMap<ServiceDomains, ServiceDomainsDto>().ReverseMap();

            CreateMap<ServiceDetails, ServiceDetailsDto>()
                .ForMember(dest => dest.Tags,
                       opt => opt.MapFrom(src => src.ServiceTags.Select(m => m.Tags).ToList()))
                .ForMember(dest => dest.Activities,
                          opt => opt.MapFrom(src => src.ServiceActivities.Select(m => m.Activities).ToList()))
                .ForMember(dest => dest.Domains,
                            opt => opt.MapFrom(src => src.ServiceDomains.Select(m => m.Domains).ToList()))
            .ReverseMap();

            CreateMap<ServiceDetails, ServiceDetailsWithAuditDto>()
                .ForMember(dest => dest.Tags,
                       opt => opt.MapFrom(src => src.ServiceTags.Select(m => m.Tags).ToList()))
                .ForMember(dest => dest.Activities,
                          opt => opt.MapFrom(src => src.ServiceActivities.Select(m => m.Activities).ToList()))
                .ForMember(dest => dest.Domains,
                            opt => opt.MapFrom(src => src.ServiceDomains.Select(m => m.Domains).ToList()))
            .ReverseMap();

            CreateMap<ServiceDetails, CreateServiceDetailsDto>().ReverseMap();
            CreateMap<ServiceDetails, UpdateServiceDetailsDto>().ReverseMap();

            CreateMap<HeaderValue, HeaderValueDto>().ReverseMap();
            CreateMap<HeaderValue, CreateOrUpdateHeaderValueDto>().ReverseMap();
            CreateMap<DocumentValue, DocumentValueDto>().ReverseMap();
            CreateMap<DocumentValue, CreateOrUpdateDocumentValueDto>().ReverseMap();


        }
    }
}
