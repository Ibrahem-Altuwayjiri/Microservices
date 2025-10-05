using AutoMapper;
using Services.AuthAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Infrastructure.Mapper
{
    public class OtpDetailsProfile : Profile
    {
        public OtpDetailsProfile()
        {
            CreateMap<OtpDetails, OtpDetailsProfile>().ReverseMap();
        }
    }
}
