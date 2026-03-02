using Services.Frontend.Web.Configuration.ExceptionHandlers;
using Services.Frontend.Web.Models.Dto;
using Services.Frontend.Web.Services.DTOs.LookupService;
using Services.Frontend.Web.Enum;

namespace Services.Frontend.Web.Services.LookupService
{
    public class TagsService : ITagsService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<TagsService> _logger;

        public TagsService(
            IBaseService baseService,
            ILogger<TagsService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<List<TagsDto>> GetTagsAsync()
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Tags/GetAll",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to get tags",
                    RestfulStatusCodes.InternalServerError);
            }

            return System.Text.Json.JsonSerializer.Deserialize<List<TagsDto>>(
                response.Result?.ToString() ?? "[]",
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<TagsDto>();
        }

        public async Task<TagsDto> GetTagsByIdAsync(int id)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Tags/GetById/{id}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? $"Tag {id} not found",
                    RestfulStatusCodes.NotFound);
            }

            return System.Text.Json.JsonSerializer.Deserialize<TagsDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<TagsDto> CreateTagsAsync(CreateOrUpdateTagsDto dto)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Tags/Create",
                Data = dto,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to create tag",
                    RestfulStatusCodes.BadRequest);
            }

            return System.Text.Json.JsonSerializer.Deserialize<TagsDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<TagsDto> UpdateTagsAsync(CreateOrUpdateTagsDto dto)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Tags/Update",
                Data = dto,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to update tag",
                    RestfulStatusCodes.BadRequest);
            }

            return System.Text.Json.JsonSerializer.Deserialize<TagsDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> DeactivateTag(int Id)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Tags/deactivate/{Id}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to Deactivate Tags",
                    RestfulStatusCodes.BadRequest);
            }
            return true;
        }
    }
}
