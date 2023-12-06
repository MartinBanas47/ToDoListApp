using Microsoft.AspNetCore.Mvc;
using ToDo_App_core.Services.User;

namespace ToDo_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetNamesAsync()
        {
            return await _userService.GetUserNamesAsync();
        }

        [HttpDelete]
        public async Task<bool> DeleteUserAsync(string name)
        {
            return await _userService.RemoveUserAsync(name);
        }

        [HttpPut]
        public async Task<bool> UpdateUserAsync(string oldName, string newName)
        {
            return await _userService.UpdateUserAsync(oldName, newName);
        }

        [HttpPost]
        public async Task<bool> CreateUser(string name)
        {
            return await _userService.CreateUserAsync(name);
        }
    }
}
