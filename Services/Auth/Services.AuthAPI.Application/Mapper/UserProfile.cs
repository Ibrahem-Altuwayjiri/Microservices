using AutoMapper;
using Services.AuthAPI.Application.Models.Dto.User;
using Services.AuthAPI.Domain.Entities;


namespace Services.AuthAPI.Application.Mapper
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
