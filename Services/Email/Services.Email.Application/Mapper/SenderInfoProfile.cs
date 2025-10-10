using AutoMapper;
using Services.Email.Application.Models.Dto.SenderInfo;

namespace Services.Email.Domain.Entities
{
    public class SenderInfoProfile : Profile
    {
        public SenderInfoProfile()
        {
            CreateMap<SenderInfo, SenderInfoDto>().ReverseMap();
            CreateMap<SenderInfo, CreateSenderInfoDto>().ReverseMap();
        }
    }
}
