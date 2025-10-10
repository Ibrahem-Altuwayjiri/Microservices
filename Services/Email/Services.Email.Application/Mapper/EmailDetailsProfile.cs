using AutoMapper;
using Services.Email.Application.Models.Dto.EmailDetails;


namespace Services.Email.Domain.Entities
{
    public class EmailDetailsProfile : Profile
    {
        public EmailDetailsProfile()
        {
            CreateMap<EmailDetails, EmailDetailsDto>().ReverseMap();
            CreateMap<EmailDetails, CreateEmailDetailsDto>().ReverseMap();
        }
    }
}
