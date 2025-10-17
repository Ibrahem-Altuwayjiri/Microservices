using AutoMapper;
using Services.FileManagement.Application.Models.Dto.MediaFile;
using Services.FileManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Application.Mapper
{
    public class MediaFileProfile : Profile
    {
        public MediaFileProfile()
        {
            //CreateMap<MediaFile, MediaFileDto>()
            //    .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName + src.Extension));
            CreateMap<MediaFile, MediaFileDto>().ReverseMap();
        }
    }
}
