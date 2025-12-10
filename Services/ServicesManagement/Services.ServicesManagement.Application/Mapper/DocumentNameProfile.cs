using AutoMapper;
using Services.ServicesManagement.Application.Models.Dto.DocumentName;
using Services.ServicesManagement.Domain.Entities.Lookups;

namespace Services.ServicesManagement.Application.Mapper
{
    public class DocumentNameProfile : Profile
    {
        public DocumentNameProfile()
        {
            CreateMap<DocumentName, DocumentNameDto>().ReverseMap();
            CreateMap<DocumentName, DocumentNameWithAuditDto>().ReverseMap();
            CreateMap<DocumentName, CreateOrUpdateDocumentNameDto>().ReverseMap();
        }
    }
}
