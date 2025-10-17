using AutoMapper;
using Services.Email.Application.Models.Dto.EmailRecipient;


namespace Services.Email.Domain.Entities
{
    public class EmailRecipientProfile : Profile
    {
        public EmailRecipientProfile()
        {
            CreateMap<EmailRecipient, EmailRecipientDto>().ReverseMap();
            CreateMap<EmailRecipient, CreateEmailRecipientDto>().ReverseMap();
        }
    }
}
