using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.AuthAPI.Application.IService;
using Services.AuthAPI.Application.Models.Dto.User;
using Services.AuthAPI.Domain.Entities;
using Services.AuthAPI.Domain.IRepositories;
using Services.AuthAPI.Infrastructure.Configuration.ExceptionHandlers;


namespace Services.AuthAPI.Application.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {

            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ApplicationUserDto> CreateNewUser(NewUserDto newUser)
        {
            
            var user = _mapper.Map<ApplicationUser>(newUser);
            var userAdded = await _userManager.CreateAsync(user, newUser.Password);
            ;
            return _mapper.Map<ApplicationUserDto>(await _userManager.FindByEmailAsync(user.Email));
        }

        public async Task<bool> DeleteUser(string Id)
        {
            var user = await _unitOfWork.ApplicationUsersRepository.FindOneOrDefault(m => m.Id == Id.Trim());
            if (user != null)
                return (await _userManager.DeleteAsync(user)).Succeeded;

            return true;
        }

        public async Task<ApplicationUserDto> UpdateUser(UpdateUserDto updateUser)
        {
            var user = await _unitOfWork.ApplicationUsersRepository.FindOneOrDefault(m => m.Id == updateUser.Id.Trim());
            if (user == null)
                throw new RestfulException("Not Found User", RestfulStatusCodes.NotFound);

            _mapper.Map(updateUser, user);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new RestfulException("Failed to update user", RestfulStatusCodes.BadRequest);

            return _mapper.Map<ApplicationUserDto>(user);
            

        }
    }
}
