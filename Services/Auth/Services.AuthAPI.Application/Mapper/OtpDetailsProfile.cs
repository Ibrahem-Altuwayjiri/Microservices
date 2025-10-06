using AutoMapper;
using Services.AuthAPI.Domain.Entities;


namespace Services.AuthAPI.Application.Mapper
{
    public class OtpDetailsProfile : Profile
    {
        public OtpDetailsProfile()
        {
            CreateMap<OtpDetails, OtpDetailsProfile>().ReverseMap();
        }
    }
}
