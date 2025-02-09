using UserService.Business.Abstraction;
using UserService.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserBusiness userBusiness, ILogger<UserController> logger)
        {
            _userBusiness = userBusiness;
            _logger = logger;
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            await _userBusiness.CreateUserAsync(dto);
            return Ok("User created successfully");
        }

        [HttpGet("{userId}", Name = "ReadUser")]
        public async Task<ActionResult<UserDto>> ReadUser(int userId)
        {
            var user = await _userBusiness.ReadUserAsync(userId);
            return user is not null ? Ok(user) : NotFound();
        }

        [HttpGet("all", Name = "GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userBusiness.GetAllUsersAsync();
            return users is not null ? Ok(users) : NotFound();
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UpdateUserDto dto)
        {
            var updatedUser = await _userBusiness.UpdateUserAsync(dto);
            return updatedUser is not null ? Ok(updatedUser) : NotFound();
        }

        [HttpDelete("{userId}", Name = "DeleteUser")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var deleted = await _userBusiness.DeleteUserAsync(userId);
            return deleted ? Ok("User deleted successfully") : NotFound();
        }
    }
}
