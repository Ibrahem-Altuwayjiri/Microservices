using Services.Frontend.Web.Configuration.ExceptionHandlers;
using Services.Frontend.Web.Models.Dto;
using Services.Frontend.Web.Services.DTOs.ManageServices;
using Services.Frontend.Web.Enum;

namespace Services.Frontend.Web.Services.ManageServices
{
    public class ServiceDetailsService : IServiceDetailsService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<ServiceDetailsService> _logger;

        public ServiceDetailsService(
            IBaseService baseService,
            ILogger<ServiceDetailsService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<List<ServiceDetailsDto>> GetAllServiceDetailsAsync()
        {
            var filteringRequest = new 
            { 
                pagination = (object)null 
            };

            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/GetAll",
                Data = filteringRequest,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to get service details",
                    RestfulStatusCodes.InternalServerError);
            }

            return System.Text.Json.JsonSerializer.Deserialize<List<ServiceDetailsDto>>(
                response.Result?.ToString() ?? "[]",
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<ServiceDetailsDto>();
        }

        public async Task<ServiceDetailsDto> GetServiceDetailsByIdAsync(string id)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/GetById/{id}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? $"Service details {id} not found",
                    RestfulStatusCodes.NotFound);
            }

            return System.Text.Json.JsonSerializer.Deserialize<ServiceDetailsDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<ServiceDetailsDto> CreateServiceDetailsAsync(CreateOrUpdateServiceDetailsDto dto)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Create",
                Data = new
                {
                    dto.NameAr,
                    dto.NameEn,
                    dto.DescriptionAr,
                    dto.DescriptionEn,
                    dto.MainServiceId,
                    dto.SubServiceId,
                    dto.SubSubServiceId,
                    dto.Activities,
                    dto.Tags,
                    dto.Domains,
                    DocumentValue = dto.DocumentsValue,
                    HeaderValue = dto.HeadersValue
                },
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true )
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to create service details",
                    RestfulStatusCodes.BadRequest);
            }
            if (response.Result == null)
                return null;

            return System.Text.Json.JsonSerializer.Deserialize<ServiceDetailsDto>(
                response.Result?.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<ServiceDetailsDto> UpdateServiceDetailsAsync(CreateOrUpdateServiceDetailsDto dto)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Update",
                Data = new
                {
                    dto.Id,
                    dto.NameAr,
                    dto.NameEn,
                    dto.DescriptionAr,
                    dto.DescriptionEn,
                    dto.MainServiceId,
                    dto.SubServiceId,
                    dto.SubSubServiceId,
                    dto.Activities,
                    dto.Tags,
                    dto.Domains,
                    DocumentValue = dto.DocumentsValue,
                    HeaderValue = dto.HeadersValue
                },
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to update service details",
                    RestfulStatusCodes.BadRequest);
            }

            return System.Text.Json.JsonSerializer.Deserialize<ServiceDetailsDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
