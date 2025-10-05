using Services.AuthAPI.Shared.Models.Dto.Abstract;
using Services.AuthAPI.Shared.Models.Dto.User;

namespace Services.AuthAPI.Shared.Models.Dto
{
    public class LoginResponseDto : BaseDto
    {
        public NewUserDto User { get; set; }
        public string Token { get; set; }
    }
}
