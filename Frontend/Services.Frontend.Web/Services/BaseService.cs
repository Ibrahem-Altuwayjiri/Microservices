using Newtonsoft.Json;
using Services.Frontend.Web.Models.Dto;
using Services.Frontend.Web.Services;
using Services.Frontend.Web.Enum;
using System.Net;
using System.Text;

namespace Services.Frontend.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseService(
            IHttpClientFactory httpClientFactory,
            ITokenProvider tokenProvider,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("GatewayClient");
                var response = await SendRequestAsync(client, requestDto, withBearer);

                // ✅ Handle token expiration (401 Unauthorized)
                if (response?.StatusCode == HttpStatusCode.Unauthorized && withBearer)
                {
                    var refreshSuccess = await RefreshTokenAsync();
                    if (refreshSuccess)
                    {
                        // Retry original request with new token
                        response = await SendRequestAsync(client, requestDto, withBearer);
                    }
                }

                return await ProcessResponseAsync(response);
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
            }
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpClient client, RequestDto requestDto, bool withBearer)
        {
            HttpRequestMessage message = new();

            if (requestDto.ContentType == SD.ContentType.MultipartFormData)
            {
                message.Headers.Add("Accept", "*/*");
            }
            else
            {
                message.Headers.Add("Accept", "application/json");
            }

            // ✅ ADD TOKEN TO AUTHORIZATION HEADER
            if (withBearer)
            {
                var token = _tokenProvider.GetToken();
                if (!string.IsNullOrEmpty(token))
                {
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }
            }

            message.RequestUri = new Uri(requestDto.Url);

            if (requestDto.ContentType == SD.ContentType.MultipartFormData)
            {
                var content = new MultipartFormDataContent();

                foreach (var prop in requestDto.Data.GetType().GetProperties())
                {
                    var value = prop.GetValue(requestDto.Data);
                    if (value is FormFile)
                    {
                        var file = (FormFile)value;
                        if (file != null)
                        {
                            content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                        }
                    }
                    else
                    {
                        content.Add(new StringContent(value == null ? "" : value.ToString()), prop.Name);
                    }
                }
                message.Content = content;
            }
            else
            {
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }
            }

            switch (requestDto.ApiType)
            {
                case SD.ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case SD.ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                case SD.ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            return await client.SendAsync(message);
        }

        // ✅ REFRESH TOKEN LOGIC
        private async Task<bool> RefreshTokenAsync()
        {
            try
            {
                // ✅ GET REFRESH TOKEN FROM COOKIES USING TokenProvider
                var refreshToken = _tokenProvider.GetRefreshToken();
                if (string.IsNullOrEmpty(refreshToken))
                    return false;

                // Call refresh token API
                HttpClient client = _httpClientFactory.CreateClient("GatewayClient");
                var refreshRequest = new HttpRequestMessage(HttpMethod.Post, $"{SD.GatewayBaseUrl}/api/Auth/RefreshToken")
                {
                    Content = new StringContent(
                        JsonConvert.SerializeObject(new { token = refreshToken }),
                        Encoding.UTF8,
                        "application/json")
                };
                refreshRequest.Headers.Add("Accept", "application/json");

                var response = await client.SendAsync(refreshRequest);

                if (response.StatusCode != HttpStatusCode.OK)
                    return false;

                var apiContent = await response.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

                if (apiResponseDto?.IsSuccess != true || apiResponseDto.Result == null)
                    return false;

                // Parse new token response
                var authResponse = JsonConvert.DeserializeObject<dynamic>(apiResponseDto.Result.ToString());
                var newToken = authResponse?.token?.ToString() ?? authResponse?.Token?.ToString();
                var newRefreshToken = authResponse?.refreshToken?.ToString() ?? authResponse?.RefreshToken?.ToString();

                if (string.IsNullOrEmpty(newToken))
                    return false;

                // ✅ UPDATE TOKEN IN COOKIES USING TokenProvider
                _tokenProvider.SetToken(newToken);

                // ✅ UPDATE REFRESH TOKEN IN COOKIES USING TokenProvider
                if (!string.IsNullOrEmpty(newRefreshToken))
                {
                    _tokenProvider.SetRefreshToken(newRefreshToken);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<ResponseDto?> ProcessResponseAsync(HttpResponseMessage? apiResponse)
        {
            if (apiResponse == null)
                return new() { IsSuccess = false, Message = "No response from server" };

            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new() { IsSuccess = false, Message = "Not Found" };
                case HttpStatusCode.Forbidden:
                    return new() { IsSuccess = false, Message = "Access Denied" };
                case HttpStatusCode.Unauthorized:
                    return new() { IsSuccess = false, Message = "Unauthorized" };
                case HttpStatusCode.InternalServerError:
                    return new() { IsSuccess = false, Message = "Internal Server Error" };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    return apiResponseDto;
            }
        }
    }
}
