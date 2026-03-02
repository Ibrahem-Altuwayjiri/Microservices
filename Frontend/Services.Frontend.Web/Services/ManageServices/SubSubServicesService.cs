using Services.Frontend.Web.Configuration.ExceptionHandlers;
using Services.Frontend.Web.Models.Dto;
using Services.Frontend.Web.Services.DTOs.ManageServices;
using Services.Frontend.Web.Enum;

namespace Services.Frontend.Web.Services.ManageServices
{
    public class SubSubServicesService : ISubSubServicesService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<SubSubServicesService> _logger;

        public SubSubServicesService(
            IBaseService baseService,
            ILogger<SubSubServicesService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<List<SubSubServiceDto>> GetSubSubServicesAsync(string filter = "")
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/SubSubService/getAll?filter={filter}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to get sub-sub services",
                    RestfulStatusCodes.InternalServerError);
            }

            var result = System.Text.Json.JsonSerializer.Deserialize<List<SubSubServiceDto>>(
                response.Result?.ToString() ?? "[]",
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<SubSubServiceDto>();



            return result;
        }

        public async Task<SubSubServiceDto> GetSubSubServiceByIdAsync(string id)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/SubSubService/getById/{id}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? $"Sub-sub service {id} not found",
                    RestfulStatusCodes.NotFound);
            }

            return System.Text.Json.JsonSerializer.Deserialize<SubSubServiceDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<SubSubServiceDto> CreateSubSubServiceAsync(CreateOrUpdateSubSubServiceDto dto)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/SubSubService/Create",
                Data = dto,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to create sub-sub service",
                    RestfulStatusCodes.BadRequest);
            }

            return System.Text.Json.JsonSerializer.Deserialize<SubSubServiceDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<SubSubServiceDto> UpdateSubSubServiceAsync(CreateOrUpdateSubSubServiceDto dto)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/SubSubService/Update",
                Data = dto,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to update sub-sub service",
                    RestfulStatusCodes.BadRequest);
            }

            return System.Text.Json.JsonSerializer.Deserialize<SubSubServiceDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> DeactivateSubSubService(string Id)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/SubSubService/deactivate/{Id}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to deactivate sub-sub service",
                    RestfulStatusCodes.BadRequest);
            }
            return true;
        }
    }
}
