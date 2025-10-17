using AutoMapper;
using Microsoft.AspNetCore.Http;
using Services.FileManagement.Application.Models.Dto;
using Services.FileManagement.Domain.Entities;
using Services.FileManagement.Domain.IRepositories;
using Services.FileManagement.Infrastructure.Configuration;
using Services.FileManagement.Infrastructure.Configuration.ExceptionHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Application.Service.Abstract
{
    public class BaseService
    {
        protected internal IMapper _mapper;
        protected internal IUnitOfWork _unitOfWork;
        protected internal IHttpContextAccessor _httpContextAccessor;

        // Path for upload file, stream or download
        protected static string FileUploadPath;


        public BaseService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;

            FileUploadPath = ConfigurationUtil.GetValue<string>("FileUploadPath");
            // Creates a directory for media files if not already exists
            var directory = Path.GetDirectoryName(FileUploadPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
        protected string FilePath(string fileName)
        {
            return Path.Combine(FileUploadPath, fileName);
        }

        protected string GenerateUniqueFileName(string originalFileName)
        {
            return Guid.NewGuid().ToString() + "_" + originalFileName;
        }

        protected async Task<MediaFile> GetById(string Id)
        {
            var MediaFile = await _unitOfWork.MediaFileRepository.FindOneOrDefaultWithInclude(m => m.Id == Id, o => o.FileDetails , o => o.UploaderInfo);
            if (MediaFile == null)
                throw new RestfulException($"Not Found File with Id: {Id}", RestfulStatusCodes.NotFound);
            if (!MediaFile.FileDetails.IsUpload)
            {
                //TODO: get byet from external storage
                //MediaFile.FileDetails.FileBytes = 
            }
            return MediaFile;
        }

        protected async Task<List<MediaFile>> GetAllByUserId(FileIds fileIds)
        {
           List<MediaFile> mediaFiles = new List<MediaFile>();
              foreach (var id in fileIds.Id)
              {
                var mediaFile = await  GetById(id);
                mediaFiles.Append(mediaFile);
            }
              return mediaFiles;
        }

    }
}
