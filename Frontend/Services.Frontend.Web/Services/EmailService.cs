using Services.Frontend.Web.Configuration.ExceptionHandlers;
using Services.Frontend.Web.Models.Dto;
using Services.Frontend.Web.Enum;

namespace Services.Frontend.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly IBaseService _baseService;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            IBaseService baseService,
            ILogger<EmailService> logger)
        {
            _baseService = baseService;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(SendEmailRequest request)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.GatewayBaseUrl}/api/Email/Send",
                Data = request,
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to send email",
                    RestfulStatusCodes.InternalServerError);
            }

            return true;
        }

        public async Task<List<TemplateDto>> GetTemplatesAsync()
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/Template/GetAll",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true)
            {
                throw new RestfulException(
                    response?.Message ?? "Failed to get templates",
                    RestfulStatusCodes.InternalServerError);
            }

            return System.Text.Json.JsonSerializer.Deserialize<List<TemplateDto>>(
                response.Result?.ToString() ?? "[]",
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<TemplateDto>();
        }

        public async Task<TemplateDto> GetTemplateByIdAsync(int id)
        {
            var requestDto = new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/Template/Get?id={id}",
                ContentType = SD.ContentType.Json
            };

            var response = await _baseService.SendAsync(requestDto);
            if (response?.IsSuccess != true || response.Result == null)
            {
                throw new RestfulException(
                    response?.Message ?? $"Template {id} not found",
                    RestfulStatusCodes.NotFound);
            }

            return System.Text.Json.JsonSerializer.Deserialize<TemplateDto>(
                response.Result.ToString(),
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
