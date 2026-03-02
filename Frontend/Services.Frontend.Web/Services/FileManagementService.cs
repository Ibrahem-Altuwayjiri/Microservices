using Services.Frontend.Web.Configuration.ExceptionHandlers;
using Services.Frontend.Web.Models.Dto;
using Services.Frontend.Web.Enum;

namespace Services.Frontend.Web.Services
{
    public class FileManagementService : IFileManagementService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<FileManagementService> _logger;

        public FileManagementService(
            IBaseService baseService,
            ILogger<FileManagementService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<bool> UploadFileAsync(IFormFile file, string purpose)
        {
            var uploadDto = new { file, purpose };
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/Upload/File",
                Data = uploadDto,
                ContentType = SD.ContentType.MultipartFormData
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to upload file",
                    RestfulStatusCodes.BadRequest);
            }

            return true;
        }

        public async Task<FileDto> DownloadFileAsync(int fileId)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/Download/File/{fileId}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? $"File {fileId} not found",
                    RestfulStatusCodes.NotFound);
            }

            return System.Text.Json.JsonSerializer.Deserialize<FileDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<FileDto>> GetFilesAsync()
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/Download/GetAll",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to get files",
                    RestfulStatusCodes.InternalServerError);
            }

            return System.Text.Json.JsonSerializer.Deserialize<List<FileDto>>(
                response.Result?.ToString() ?? "[]",
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<FileDto>();
        }
    }
}
