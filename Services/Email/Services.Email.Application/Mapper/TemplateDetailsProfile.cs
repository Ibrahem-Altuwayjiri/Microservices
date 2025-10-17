using AutoMapper;
using Services.Email.Application.Models.Dto.TemplateDetails;


namespace Services.Email.Domain.Entities
{
    public class TemplateDetailsProfile : Profile
    {
        public TemplateDetailsProfile()
        {
            CreateMap<TemplateDetails, TemplateDetailsDto>().ReverseMap();
            CreateMap<TemplateDetails, CreateTemplateDetailsDto>().ReverseMap();
        }
    }
}
