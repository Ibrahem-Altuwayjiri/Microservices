using Services.Auth.Application.Models.Dto.User;


namespace Services.Auth.Application.IService
{
    public interface IUserService
    {
        Task<ApplicationUserDto> CreateNewUser(NewUserDto newUser);
        Task<ApplicationUserDto> UpdateUser(UpdateUserDto updateUser);
        Task<bool> DeleteUser(string Id);
    }
}
