using Construction.DomainModel;
using Construction.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Construction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors] // Enable CORS for all actions in this controller
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-username/{userName}")]
        public async Task<ActionResult<User>> GetUserByName(string userName)
        {
            try
            {
                var user = await _userService.GetUserByNameAsync(userName);
                if (user == null)
                {
                    return NotFound($"User with username {userName} not found.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-role/{roleId}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByRole(long roleId)
        {
            try
            {
                var users = await _userService.GetUsersByRoleAsync(roleId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("validate")]
        public async Task<ActionResult<User>> ValidateUser([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (loginRequest == null || string.IsNullOrEmpty(loginRequest.UserName) || string.IsNullOrEmpty(loginRequest.Password))
                {
                    return BadRequest("Username and password are required.");
                }

                var user = await _userService.ValidateUserAsync(loginRequest.UserName, loginRequest.Password);
                if (user == null)
                {
                    return Unauthorized("Invalid username or password.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User data is required.");
                }

                var userId = await _userService.CreateUserAsync(user);
                return CreatedAtAction(nameof(GetUser), new { id = userId }, userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(long id, [FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User data is required.");
                }

                if (id != user.ID_Users)
                {
                    return BadRequest("ID mismatch.");
                }

                var result = await _userService.UpdateUserAsync(user);
                if (!result)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(long id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (!result)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
