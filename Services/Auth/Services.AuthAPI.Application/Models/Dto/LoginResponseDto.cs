using Services.AuthAPI.Application.Models.Dto.Abstract;
using Services.AuthAPI.Application.Models.Dto.User;


namespace Services.AuthAPI.Application.Models.Dto
{
    public class LoginResponseDto : BaseDto
    {
        public NewUserDto User { get; set; }
        public string Token { get; set; }
    }
}
