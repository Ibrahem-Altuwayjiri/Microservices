using AutoMapper;
using Services.AuthAPI.Domain.Entities;
using Services.AuthAPI.Shared.Models.Dto.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Infrastructure.Mapper
{
    public class TokenDetailsProfile : Profile
    {
        public TokenDetailsProfile()
        {
            CreateMap<TokenDetails, TokenDetailsDto>().ReverseMap();
        }        
    }
}
