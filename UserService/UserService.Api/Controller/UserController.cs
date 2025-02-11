using UserService.Business.Abstraction;
using UserService.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]/[action]")]// Definisce il percorso base dell'API
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly ILogger<UserController> _logger;
         // Iniezione delle dipendenze per la logica di business e il logging
        public UserController(IUserBusiness userBusiness, ILogger<UserController> logger)
        {
            _userBusiness = userBusiness;
            _logger = logger;
        }
         // Endpoint per creare un nuovo utente
        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            await _userBusiness.CreateUserAsync(dto);
            return Ok("User created successfully");
        }
        // Endpoint per leggere un solo utente inserendo l'id
        [HttpGet("{userId}", Name = "ReadUser")]
        public async Task<ActionResult<UserDto>> ReadUser(int userId)
        {
            var user = await _userBusiness.ReadUserAsync(userId);
            return user is not null ? Ok(user) : NotFound();
        }
        // Endpoint per leggere tutti gli utenti
        [HttpGet("all", Name = "GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userBusiness.GetAllUsersAsync();
            return users is not null ? Ok(users) : NotFound();
        }
        // Endpoint per aggiornare un utente
        [HttpPut(Name = "UpdateUser")]
        public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UpdateUserDto dto)
        {
            var updatedUser = await _userBusiness.UpdateUserAsync(dto);
            return updatedUser is not null ? Ok(updatedUser) : NotFound();
        }
        // Endpoint per eliminare un utente
        [HttpDelete("{userId}", Name = "DeleteUser")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var deleted = await _userBusiness.DeleteUserAsync(userId);
            return deleted ? Ok("User deleted successfully") : NotFound();
        }
    }
}
