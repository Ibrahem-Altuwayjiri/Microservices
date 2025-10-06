using AutoMapper;
using Services.Auth.Application.Models.Dto.Token;
using Services.Auth.Domain.Entities;


namespace Services.Auth.Application.Mapper
{
    public class TokenDetailsProfile : Profile
    {
        public TokenDetailsProfile()
        {
            CreateMap<TokenDetails, TokenDetailsDto>().ReverseMap();
        }        
    }
}
