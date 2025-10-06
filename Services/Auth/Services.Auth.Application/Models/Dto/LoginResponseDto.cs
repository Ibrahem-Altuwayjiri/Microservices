using Services.Auth.Application.Models.Dto.Abstract;
using Services.Auth.Application.Models.Dto.User;


namespace Services.Auth.Application.Models.Dto
{
    public class LoginResponseDto : BaseDto
    {
        public NewUserDto User { get; set; }
        public string Token { get; set; }
    }
}
