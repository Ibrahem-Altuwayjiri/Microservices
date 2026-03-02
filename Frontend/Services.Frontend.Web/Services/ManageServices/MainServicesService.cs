using Services.Frontend.Web.Configuration.ExceptionHandlers;
using Services.Frontend.Web.Models.Dto;
using Services.Frontend.Web.Services.DTOs.ManageServices;
using Services.Frontend.Web.Enum;

namespace Services.Frontend.Web.Services.ManageServices
{
    public class MainServicesService : IMainServicesService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<MainServicesService> _logger;

        public MainServicesService(
            IBaseService baseService,
            ILogger<MainServicesService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<List<MainServiceDto>> GetMainServicesAsync(string filter = "")
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/MainService/GetAll?filter={filter}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to get main services",
                    RestfulStatusCodes.InternalServerError);
            }

            var result = System.Text.Json.JsonSerializer.Deserialize<List<MainServiceDto>>(
                response.Result?.ToString() ?? "[]",
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<MainServiceDto>();



            return result;
        }

        public async Task<MainServiceDto> GetMainServiceByIdAsync(string id)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/MainService/GetById/{id}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? $"Main service {id} not found",
                    RestfulStatusCodes.NotFound);
            }

            return System.Text.Json.JsonSerializer.Deserialize<MainServiceDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<MainServiceDto> CreateMainServiceAsync(CreateOrUpdateMainServiceDto dto)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/MainService/Create",
                Data = dto,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to create main service",
                    RestfulStatusCodes.BadRequest);
            }

            return System.Text.Json.JsonSerializer.Deserialize<MainServiceDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<MainServiceDto> UpdateMainServiceAsync(CreateOrUpdateMainServiceDto dto)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/MainService/Update",
                Data = dto,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to update main service",
                    RestfulStatusCodes.BadRequest);
            }

            return System.Text.Json.JsonSerializer.Deserialize<MainServiceDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> DeactivateMainService(string Id)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/MainService/deactivate/{Id}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to Deactivate Main Service",
                    RestfulStatusCodes.BadRequest);
            }
            return true;
        }
    }
}
