using Construction.DomainModel;
using Construction.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Construction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors] // Enable CORS for all actions in this controller
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetAllUserRoles()
        {
            try
            {
                var userRoles = await _userRoleService.GetAllUserRolesAsync();
                return Ok(userRoles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRole>> GetUserRole(long id)
        {
            try
            {
                var userRole = await _userRoleService.GetUserRoleByIdAsync(id);
                if (userRole == null)
                {
                    return NotFound($"User role with ID {id} not found.");
                }
                return Ok(userRole);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-name/{roleName}")]
        public async Task<ActionResult<UserRole>> GetUserRoleByName(string roleName)
        {
            try
            {
                var userRole = await _userRoleService.GetUserRoleByNameAsync(roleName);
                if (userRole == null)
                {
                    return NotFound($"User role with name {roleName} not found.");
                }
                return Ok(userRole);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateUserRole([FromBody] UserRole userRole)
        {
            try
            {
                if (userRole == null)
                {
                    return BadRequest("User role data is required.");
                }

                var userRoleId = await _userRoleService.CreateUserRoleAsync(userRole);
                return CreatedAtAction(nameof(GetUserRole), new { id = userRoleId }, userRoleId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserRole(long id, [FromBody] UserRole userRole)
        {
            try
            {
                if (userRole == null)
                {
                    return BadRequest("User role data is required.");
                }

                if (id != userRole.ID_UserRole)
                {
                    return BadRequest("ID mismatch.");
                }

                var result = await _userRoleService.UpdateUserRoleAsync(userRole);
                if (!result)
                {
                    return NotFound($"User role with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserRole(long id)
        {
            try
            {
                var result = await _userRoleService.DeleteUserRoleAsync(id);
                if (!result)
                {
                    return NotFound($"User role with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
