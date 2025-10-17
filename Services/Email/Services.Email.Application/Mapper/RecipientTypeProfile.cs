using AutoMapper;
using Services.Email.Application.Models.Dto.RecipientType;


namespace Services.Email.Domain.Entities
{
    public class RecipientTypeProfile : Profile
    {
        public RecipientTypeProfile()
        {
            CreateMap<RecipientType, RecipientTypeDto>().ReverseMap();
        }
    }
}
