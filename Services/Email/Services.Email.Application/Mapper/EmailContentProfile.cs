using AutoMapper;
using Services.Email.Application.Models.Dto.EmailContent;


namespace Services.Email.Domain.Entities
{
    public class EmailContentProfile : Profile
    {
        public EmailContentProfile()
        {
            CreateMap<EmailContent, EmailContentDto>().ReverseMap();
            CreateMap<EmailContent, CreateEmailContentDto>().ReverseMap();
        }
    }
}
