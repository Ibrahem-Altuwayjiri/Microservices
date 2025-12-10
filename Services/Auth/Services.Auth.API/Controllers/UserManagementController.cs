using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Auth.Application.IService;
using Services.Auth.Application.Models.Dto.User;


namespace Services.AuthAPI.Controllers
{
    [Route("api/Auth/[controller]")]
    [ApiController]
    [Authorize]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserManagementController(IUserService userService, IHttpClientFactory httpClientFactory)
        {
            _userService = userService;
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] NewUserDto newUser)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _userService.CreateNewUser(newUser));
        }

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUser)
        {
            return Ok(await _userService.UpdateUser(updateUser));
        }

        [HttpPost("DeleteUser")]
        
        public async Task<IActionResult> DeleteUser([FromBody] string Id)
        {
            return Ok(await _userService.DeleteUser(Id));
        }
    }
}
