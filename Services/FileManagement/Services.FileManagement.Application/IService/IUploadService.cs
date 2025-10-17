using Microsoft.AspNetCore.Http;
using Services.FileManagement.Application.Models.Dto.MediaFile;
using Services.FileManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Application.IService
{
    public interface IUploadService
    {
        Task<MediaFileDto> UploadFileAsync(IFormFile fileToUpload);
        Task<List<MediaFileDto>> UploadFilesAsync(List<IFormFile> filesToUpload);
        Task SaveFile(MediaFile mediaFile, IFormFile fileToUpload);
        Task SaveFileAsBytes(MediaFile mediaFile);
        Task SaveFiles();
    }
}
