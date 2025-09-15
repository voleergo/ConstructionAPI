using Construction.DomainModel;
using Construction.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Construction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectLevelController : ControllerBase
    {
        private readonly IProjectLevelService _projectLevelService;

        public ProjectLevelController(IProjectLevelService projectLevelService)
        {
            _projectLevelService = projectLevelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectLevel>>> GetAllProjectLevels()
        {
            try
            {
                var projectLevels = await _projectLevelService.GetAllProjectLevelsAsync();
                return Ok(projectLevels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectLevel>> GetProjectLevel(long id)
        {
            try
            {
                var projectLevel = await _projectLevelService.GetProjectLevelByIdAsync(id);
                if (projectLevel == null)
                {
                    return NotFound($"Project level with ID {id} not found.");
                }
                return Ok(projectLevel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectLevel>>> GetProjectLevelsByProject(long projectId)
        {
            try
            {
                var projectLevels = await _projectLevelService.GetProjectLevelsByProjectIdAsync(projectId);
                return Ok(projectLevels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-level/{levelId}")]
        public async Task<ActionResult<IEnumerable<ProjectLevel>>> GetProjectLevelsByLevel(long levelId)
        {
            try
            {
                var projectLevels = await _projectLevelService.GetProjectLevelsByLevelIdAsync(levelId);
                return Ok(projectLevels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateProjectLevel([FromBody] ProjectLevel projectLevel)
        {
            try
            {
                if (projectLevel == null)
                {
                    return BadRequest("Project level data is required.");
                }

                var projectLevelId = await _projectLevelService.CreateProjectLevelAsync(projectLevel);
                return CreatedAtAction(nameof(GetProjectLevel), new { id = projectLevelId }, projectLevelId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProjectLevel(long id, [FromBody] ProjectLevel projectLevel)
        {
            try
            {
                if (projectLevel == null)
                {
                    return BadRequest("Project level data is required.");
                }

                if (id != projectLevel.ID_ProjectLevel)
                {
                    return BadRequest("ID mismatch.");
                }

                var result = await _projectLevelService.UpdateProjectLevelAsync(projectLevel);
                if (!result)
                {
                    return NotFound($"Project level with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectLevel(long id)
        {
            try
            {
                var result = await _projectLevelService.DeleteProjectLevelAsync(id);
                if (!result)
                {
                    return NotFound($"Project level with ID {id} not found.");
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
