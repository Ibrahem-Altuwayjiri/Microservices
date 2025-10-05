using Services.AuthAPI.Shared.Models.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Application.IService
{
    public interface IUserService
    {
        Task<ApplicationUserDto> CreateNewUser(NewUserDto newUser);
        Task<ApplicationUserDto> UpdateUser(UpdateUserDto updateUser);
        Task<bool> DeleteUser(string Id);
    }
}
