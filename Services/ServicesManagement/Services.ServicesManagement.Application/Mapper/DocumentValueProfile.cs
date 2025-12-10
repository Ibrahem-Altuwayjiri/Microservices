using AutoMapper;
using Services.ServicesManagement.Application.Models.Dto.DocumentName;
using Services.ServicesManagement.Application.Models.Dto.ServiceInfo;
using Services.ServicesManagement.Domain.Entities.Lookups;
using Services.ServicesManagement.Domain.Entities.ServiceInfo;

namespace Services.ServicesManagement.Application.Mapper
{
    public class DocumentValueProfile : Profile
    {
        public DocumentValueProfile()
        {
            CreateMap<DocumentValue, DocumentValueDto>().ReverseMap();

        }
    }
}
