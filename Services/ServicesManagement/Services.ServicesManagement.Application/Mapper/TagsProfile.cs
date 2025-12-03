using AutoMapper;
using Services.ServicesManagement.Application.Models.Dto.Tags;
using Services.ServicesManagement.Domain.Entities.Lookups;

namespace Services.ServicesManagement.Application.Mapper
{
    public class TagsProfile : Profile
    {
        public TagsProfile()
        {
            CreateMap<Tags, TagsDto>().ReverseMap();
            CreateMap<Tags, TagsWithAuditDto>().ReverseMap();
            CreateMap<Tags, CreateOrUpdateTagsDto>().ReverseMap();
        }
    }
}
