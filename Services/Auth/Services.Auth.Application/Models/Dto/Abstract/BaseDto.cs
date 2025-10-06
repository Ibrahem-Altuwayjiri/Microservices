

namespace Services.Auth.Application.Models.Dto.Abstract
{
    public class BaseDto
    {
        public bool IsSuccess { get; set; } = true;
        public string? ErrorMessage { get; set; }
    }
}
