using AutoMapper;
using Services.AuthAPI.Domain.Entities;
using Services.AuthAPI.Shared.Models.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Infrastructure.Mapper
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
