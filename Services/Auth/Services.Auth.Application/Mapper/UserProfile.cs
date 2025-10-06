using AutoMapper;
using Services.Auth.Application.Models.Dto.User;
using Services.Auth.Domain.Entities;


namespace Services.Auth.Application.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser,ApplicationUserDto>().ReverseMap();
            CreateMap<ApplicationUser, NewUserDto>()
                .ForMember(dest => dest.Email , opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
            CreateMap<ApplicationUser, UpdateUserDto>().ReverseMap();
        }
    }
}
