using AutoMapper;
using Services.Auth.Domain.Entities;


namespace Services.Auth.Application.Mapper
{
    public class OtpDetailsProfile : Profile
    {
        public OtpDetailsProfile()
        {
            CreateMap<OtpDetails, OtpDetailsProfile>().ReverseMap();
        }
    }
}
