using AutoMapper;
using Services.Email.Application.Models.Dto.Template;


namespace Services.Email.Domain.Entities
{
    public class TemplateProfile : Profile
    {
        public TemplateProfile()
        {
            CreateMap<Template, TemplateDto>()
            .ForMember(dest => dest.templateDetails,
                       opt => opt.MapFrom(src => src.TemplateDetails.FirstOrDefault(td => td.IsActive)))
            .ReverseMap();

            CreateMap<CreateTemplateDto, Template>()
                .ForMember(dest => dest.TemplateDetails, opt => opt.Ignore());

            CreateMap<Template, CreateTemplateDto>();
            
            CreateMap<Template, UpdateTemplateDto>().ReverseMap();
        }
    }
}
