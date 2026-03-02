using Services.Frontend.Web.Configuration.ExceptionHandlers;
using Services.Frontend.Web.Models.Dto;
using Services.Frontend.Web.Services.DTOs.LookupService;
using Services.Frontend.Web.Enum;

namespace Services.Frontend.Web.Services.LookupService
{
    public class DomainsService : IDomainsService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<DomainsService> _logger;

        public DomainsService(
            IBaseService baseService,
            ILogger<DomainsService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<List<DomainsDto>> GetDomainsAsync()
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Domains/GetAll",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to get domains",
                    RestfulStatusCodes.InternalServerError);
            }

            return System.Text.Json.JsonSerializer.Deserialize<List<DomainsDto>>(
                response.Result?.ToString() ?? "[]",
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<DomainsDto>();
        }

        public async Task<DomainsDto> GetDomainsByIdAsync(int id)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Domains/GetById/{id}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? $"Domain {id} not found",
                    RestfulStatusCodes.NotFound);
            }

            return System.Text.Json.JsonSerializer.Deserialize<DomainsDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<DomainsDto> CreateDomainsAsync(CreateOrUpdateDomainsDto dto)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Domains/Create",
                Data = dto,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to create domain",
                    RestfulStatusCodes.BadRequest);
            }

            return System.Text.Json.JsonSerializer.Deserialize<DomainsDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<DomainsDto> UpdateDomainsAsync(CreateOrUpdateDomainsDto dto)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Domains/Update",
                Data = dto,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to update domain",
                    RestfulStatusCodes.BadRequest);
            }

            return System.Text.Json.JsonSerializer.Deserialize<DomainsDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> DeactivateDomain(int Id)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/ServiceInfo/Domains/deactivate/{Id}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to Deactivate Domains",
                    RestfulStatusCodes.BadRequest);
            }
            return true;
        }
    }
}
