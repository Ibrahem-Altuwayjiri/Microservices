using Services.Frontend.Web.Configuration.ExceptionHandlers;
using Services.Frontend.Web.Models.Dto;
using Services.Frontend.Web.Services.DTOs.LookupService;
using Services.Frontend.Web.Enum;

namespace Services.Frontend.Web.Services.LookupService
{
    public class HeadersService : IHeadersService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<HeadersService> _logger;

        public HeadersService(
            IBaseService baseService,
            ILogger<HeadersService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<List<HeaderDto>> GetHeadersAsync()
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Header/GetAll",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to get headers",
                    RestfulStatusCodes.InternalServerError);
            }

            return System.Text.Json.JsonSerializer.Deserialize<List<HeaderDto>>(
                response.Result?.ToString() ?? "[]",
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<HeaderDto>();
        }

        public async Task<HeaderDto> GetHeadersByIdAsync(int id)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Header/GetById/{id}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? $"Header {id} not found",
                    RestfulStatusCodes.NotFound);
            }

            return System.Text.Json.JsonSerializer.Deserialize<HeaderDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<HeaderDto> CreateHeadersAsync(CreateOrUpdateHeaderDto dto)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Header/Create",
                Data = dto,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to create header",
                    RestfulStatusCodes.BadRequest);
            }

            return System.Text.Json.JsonSerializer.Deserialize<HeaderDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<HeaderDto> UpdateHeadersAsync(CreateOrUpdateHeaderDto dto)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Header/Update",
                Data = dto,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to update header",
                    RestfulStatusCodes.BadRequest);
            }

            return System.Text.Json.JsonSerializer.Deserialize<HeaderDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> DeactivateHeader(int Id)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Header/deactivate/{Id}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to Deactivate Header",
                    RestfulStatusCodes.BadRequest);
            }
            return true;
        }
    }
}
