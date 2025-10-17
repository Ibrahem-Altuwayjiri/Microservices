
using AutoMapper;
using Services.Email.Application.Models.Dto.Attachments;
using Services.Email.Domain.Entities;

namespace Services.Email.Application.Mapper
{
    public class AttachmentsProfile : Profile
    {
        public AttachmentsProfile()
        {
            CreateMap<Attachments, AttachmentsDto>().ReverseMap();
            CreateMap<Attachments, CreateAttachmentsDto>().ReverseMap();
        }
    }
}
