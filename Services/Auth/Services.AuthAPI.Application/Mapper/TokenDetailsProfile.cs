using AutoMapper;
using Services.AuthAPI.Application.Models.Dto.Token;
using Services.AuthAPI.Domain.Entities;


namespace Services.AuthAPI.Application.Mapper
{
    public class TokenDetailsProfile : Profile
    {
        public TokenDetailsProfile()
        {
            CreateMap<TokenDetails, TokenDetailsDto>().ReverseMap();
        }        
    }
}
