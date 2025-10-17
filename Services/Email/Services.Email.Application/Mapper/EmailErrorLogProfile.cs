using AutoMapper;
using Services.Email.Application.Models.Dto.EmailErrorLog;


namespace Services.Email.Domain.Entities
{
    public class EmailErrorLogProfile : Profile
    {
        public EmailErrorLogProfile()
        {
            CreateMap<EmailErrorLog, EmailErrorLogDto>().ReverseMap();
            CreateMap<EmailErrorLog, CreateEmailErrorLogDto>().ReverseMap();
        }
    }
}
