using AutoMapper;
using Microsoft.AspNetCore.Http;
using Services.FileManagement.Application.IService;
using Services.FileManagement.Application.Job;
using Services.FileManagement.Application.Models.Dto;
using Services.FileManagement.Application.Models.Dto.MediaFile;
using Services.FileManagement.Application.Service.Abstract;
using Services.FileManagement.Domain.Entities;
using Services.FileManagement.Domain.IRepositories;
using Services.FileManagement.Infrastructure.Configuration.ExceptionHandlers;
using Services.FileManagement.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileManagement.Application.Service
{
    public class UploadService : BaseService , IUploadService
    {
        private readonly UploadFileJob _uploadFileJob;

        public UploadService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, UploadFileJob uploadFileJob)
            : base(unitOfWork, httpContextAccessor, mapper)
        {
            _uploadFileJob = uploadFileJob;
        }



        public async Task<MediaFileDto> UploadFileAsync(IFormFile fileToUpload)
        {
            string fileName = GenerateUniqueFileName(fileToUpload.FileName);
            string fileUploadPath = FilePath(fileName);

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim?.Subject?.Claims.FirstOrDefault(u => u.Properties.Values.Any(x => x.Equals("sub")))?.Value;

            var  uploaderInfo = await  _unitOfWork.UploaderInfoRepository.Add(new UploaderInfo
            {
                ClientIp = ClientInfoHelper.GetClientIp(_httpContextAccessor.HttpContext),
                UserId = userId
            });

            var fileDetails = await _unitOfWork.TempFileInfoRepository.Add(new FileDetails
            {
                ExternalStorageReferenceId = fileUploadPath,
                FileBytes = await ReadFileToBytesAsync(fileToUpload),

            });

            var mediaFile = await _unitOfWork.MediaFileRepository.Add(new MediaFile
            {
                FileName = fileName,
                Extension = Path.GetExtension(fileToUpload.FileName),
                MimeType = fileToUpload.ContentType,
                UploadDate = DateTime.UtcNow,
                FileDetailsId = fileDetails.Id,
                UploaderInfoId = uploaderInfo.Id,
                //ServiceKey = "TODO"
            });

            await _uploadFileJob.EnqueueAsync();


            await _unitOfWork.CompletedAsync();
            return _mapper.Map<MediaFileDto>(mediaFile);

        }

        public async Task<List<MediaFileDto>> UploadFilesAsync(List<IFormFile> filesToUpload)
        {
            List<MediaFileDto> uploadedFiles = new List<MediaFileDto>();
            foreach (var file in filesToUpload)
            {
                uploadedFiles.Add(await UploadFileAsync(file));
            }
            return _mapper.Map<List<MediaFileDto>>(uploadedFiles);
        }

        public async Task SaveFile(MediaFile mediaFile , IFormFile fileToUpload)
        {
            string filePath = mediaFile.FileDetails.ExternalStorageReferenceId;
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await fileToUpload.CopyToAsync(fileStream);
            }
        }

        public async Task SaveFileAsBytes(MediaFile mediaFile)
        {
            byte[] fileBytes = mediaFile.FileDetails.FileBytes;
            string filePath = mediaFile.FileDetails.ExternalStorageReferenceId;
            var fileDetails = await _unitOfWork.TempFileInfoRepository.FindOneOrDefault(m => m.Id == mediaFile.FileDetailsId);
            try
            {
                await File.WriteAllBytesAsync(filePath, fileBytes);
                fileDetails.IsUpload = true;
                fileDetails.UploadDate = DateTime.UtcNow.ToLocalTime();
                fileDetails.FileBytes = null; // Clear the byte array to free up memory
                await _unitOfWork.TempFileInfoRepository.Update(fileDetails);
                await _unitOfWork.CompletedAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.UploaderErrorLogRepository.Add(new UploaderErrorLog
                {
                    MediaFileId = mediaFile.Id,
                    Date = DateTime.UtcNow.ToLocalTime(),
                    Message = e.Message
                });
                
                fileDetails.TryNum += 1;
                fileDetails.LastTrySend = DateTime.UtcNow.ToLocalTime();
                await _unitOfWork.TempFileInfoRepository.Update(fileDetails);

                await _unitOfWork.CompletedAsync();
            }

        }

        public async Task SaveFiles()
        {
            var UnUpload = await GetAllUnUploaded();
            if (UnUpload == null )
                return;

            foreach (var item in UnUpload)
            {
                await SaveFileAsBytes(item);
            }
        }

        private async Task<IEnumerable<MediaFile>> GetAllUnUploaded()
        {
            var MediaFile = await _unitOfWork.MediaFileRepository.FindWithInclude(m => !m.FileDetails.IsUpload, o => o.FileDetails, o => o.UploaderInfo);
            if (MediaFile == null)
                return null;

            return MediaFile;
        }
        private static async Task<byte[]> ReadFileToBytesAsync(IFormFile file) 
        { 
            using var ms = new MemoryStream(); 
            await file.CopyToAsync(ms);
            return ms.ToArray(); 
        }



    }
}
