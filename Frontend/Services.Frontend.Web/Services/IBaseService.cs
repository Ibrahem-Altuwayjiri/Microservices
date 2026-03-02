using Services.Frontend.Web.Models.Dto;

namespace Services.Frontend.Web.Services
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
