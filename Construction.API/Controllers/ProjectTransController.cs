using Construction.DomainModel;
using Construction.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Construction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTransController : ControllerBase
    {
        private readonly IProjectTransService _projectTransService;

        public ProjectTransController(IProjectTransService projectTransService)
        {
            _projectTransService = projectTransService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTrans>>> GetAllProjectTrans()
        {
            try
            {
                var projectTrans = await _projectTransService.GetAllProjectTransAsync();
                return Ok(projectTrans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTrans>> GetProjectTrans(long id)
        {
            try
            {
                var projectTrans = await _projectTransService.GetProjectTransByIdAsync(id);
                if (projectTrans == null)
                {
                    return NotFound($"Project transaction with ID {id} not found.");
                }
                return Ok(projectTrans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectTrans>>> GetProjectTransByProject(long projectId)
        {
            try
            {
                var projectTrans = await _projectTransService.GetProjectTransByProjectIdAsync(projectId);
                return Ok(projectTrans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-level/{levelId}")]
        public async Task<ActionResult<IEnumerable<ProjectTrans>>> GetProjectTransByLevel(long levelId)
        {
            try
            {
                var projectTrans = await _projectTransService.GetProjectTransByLevelIdAsync(levelId);
                return Ok(projectTrans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-item/{itemId}")]
        public async Task<ActionResult<IEnumerable<ProjectTrans>>> GetProjectTransByItem(long itemId)
        {
            try
            {
                var projectTrans = await _projectTransService.GetProjectTransByItemIdAsync(itemId);
                return Ok(projectTrans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-account-type/{accountType}")]
        public async Task<ActionResult<IEnumerable<ProjectTrans>>> GetProjectTransByAccountType(string accountType)
        {
            try
            {
                var projectTrans = await _projectTransService.GetProjectTransByAccountTypeAsync(accountType);
                return Ok(projectTrans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateProjectTrans([FromBody] ProjectTrans projectTrans)
        {
            try
            {
                if (projectTrans == null)
                {
                    return BadRequest("Project transaction data is required.");
                }

                var projectTransId = await _projectTransService.CreateProjectTransAsync(projectTrans);
                return CreatedAtAction(nameof(GetProjectTrans), new { id = projectTransId }, projectTransId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProjectTrans(long id, [FromBody] ProjectTrans projectTrans)
        {
            try
            {
                if (projectTrans == null)
                {
                    return BadRequest("Project transaction data is required.");
                }

                if (id != projectTrans.ID_ProjectTrans)
                {
                    return BadRequest("ID mismatch.");
                }

                var result = await _projectTransService.UpdateProjectTransAsync(projectTrans);
                if (!result)
                {
                    return NotFound($"Project transaction with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectTrans(long id)
        {
            try
            {
                var result = await _projectTransService.DeleteProjectTransAsync(id);
                if (!result)
                {
                    return NotFound($"Project transaction with ID {id} not found.");
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
