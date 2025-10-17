using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.FileManagement.Application.IService;
using Services.FileManagement.Application.Service.Abstract;
using Services.FileManagement.Domain.Entities;
using Services.FileManagement.Domain.IRepositories;
using Services.FileManagement.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Application.Service
{
    public class DownloadService : BaseService , IDownloadService 
    {
        // MimeType used for downloading files 
        private static string OctetStreamMimeStype;
        public DownloadService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) 
            : base(unitOfWork, httpContextAccessor, mapper)
        {
            
            OctetStreamMimeStype = "application/octet-stream";
        }

        public async Task<FileStreamResult> Download(string Id)
        {
           

            MediaFile file = await GetById(Id);
            await SaveDownloaderInfo(file.Id);
            string filePath = FilePath(file.FileName);

            //if file is stored in database as byte array (not uploaded to local storage yet by Job)
            if (file.FileDetails.FileBytes != null && file.FileDetails.FileBytes.Length > 0)
            {
                var stream = new MemoryStream(file.FileDetails.FileBytes);
                return new FileStreamResult(stream, OctetStreamMimeStype)
                {
                    FileDownloadName = file.FileName.Split('_')[1],
                };
            }
            return new FileStreamResult(new FileStream(filePath, FileMode.Open), OctetStreamMimeStype)
            {
                FileDownloadName = file.FileName.Split('_')[1],
            };
        }

        public async Task<string> DownloadAsBase64(string Id)
        {
            
            MediaFile file = await GetById(Id);
            await SaveDownloaderInfo(file.Id);

            string filePath = FilePath(file.FileName);

            if (file.FileDetails.FileBytes != null && file.FileDetails.FileBytes.Length > 0)
            {
                return Convert.ToBase64String(file.FileDetails.FileBytes);
            }
            else
            {
                byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
                return Convert.ToBase64String(fileBytes);
            }

        }

        private async Task SaveDownloaderInfo(string MediaFileId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim?.Subject?.Claims.FirstOrDefault(u => u.Properties.Values.Any(x => x.Equals("sub")))?.Value;

            var uploaderInfo = await _unitOfWork.DownloaderInfoRepository.Add(new DownloaderInfo
            {
                ClientIp = IpHelper.GetClientIp(_httpContextAccessor.HttpContext),
                UserId = userId,
                MediaFileId = MediaFileId
            });
        }
    }
}
