using Construction.DomainModel;
using Construction.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Construction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors] // Enable CORS for all actions in this controller
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetAllProjects()
        {
            try
            {
                var projects = await _projectService.GetAllProjectsAsync();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectModel>> GetProject(long id)
        {
            try
            {
                var project = await _projectService.GetProjectByIdAsync(id);
                if (project == null)
                {
                    return NotFound($"Project with ID {id} not found.");
                }
                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-code/{projectCode}")]
        public async Task<ActionResult<ProjectModel>> GetProjectByCode(string projectCode)
        {
            try
            {
                var project = await _projectService.GetProjectByCodeAsync(projectCode);
                if (project == null)
                {
                    return NotFound($"Project with code {projectCode} not found.");
                }
                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjectsByCustomer(long customerId)
        {
            try
            {
                var projects = await _projectService.GetProjectsByCustomerAsync(customerId);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-status/{status}")]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjectsByStatus(string status)
        {
            try
            {
                var projects = await _projectService.GetProjectsByStatusAsync(status);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<long>> CreateProject([FromBody] ProjectModel project)
        {
            try
            {
                if (project == null)
                {
                    return BadRequest("Project data is required.");
                }

                var projectId = await _projectService.CreateProjectAsync(project);
                return CreatedAtAction(nameof(GetProject), new { id = projectId }, projectId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProject(long id, [FromBody] ProjectModel project)
        {
            try
            {
                if (project == null)
                {
                    return BadRequest("Project data is required.");
                }

                if (id != project.ID_Project)
                {
                    return BadRequest("ID mismatch.");
                }

                var result = await _projectService.UpdateProjectAsync(project);
                if (!result)
                {
                    return NotFound($"Project with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(long id)
        {
            try
            {
                var result = await _projectService.DeleteProjectAsync(id);
                if (!result)
                {
                    return NotFound($"Project with ID {id} not found.");
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
