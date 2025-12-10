using AutoMapper;
using Services.ServicesManagement.Application.Models.Dto.Activities;
using Services.ServicesManagement.Domain.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.Mapper
{
    public class ActivitiesProfile : Profile
    {
        public ActivitiesProfile()
        {
            CreateMap<Activities, ActivitiesDto>().ReverseMap();
            CreateMap<Activities, ActivitiesWithAuditDto>().ReverseMap();
            CreateMap<Activities, CreateOrUpdateActivitiesDto>().ReverseMap();

        }
    }
}
