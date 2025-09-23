using Construction.DomainModel;
using Construction.DomainModel.User;
using Construction.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Construction.DomainModel.Project;

namespace Construction.API.Controllers
{
    [Route("v1/[action]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private const string connectstring = "ConnectionString:DefaultConnection";

        private readonly IConfiguration _configuration;
        private readonly IProjectService _projectService;
        private readonly ITokenService _tokenService;
        private readonly ILoggerService _logger;
        private readonly IWebHostEnvironment _environment;


        public ProjectController(
            IConfiguration configuration,
            IProjectService projectService,
            ITokenService tokenService,
            ILoggerService loggerService,
            IWebHostEnvironment environment

        ) : base(configuration)
        {
            _configuration = configuration;
            _projectService = projectService;
            _tokenService = tokenService;
            _logger = loggerService;
            _environment = environment;

            _projectService.ConnectionStrings = configuration[connectstring];
            _logger.ConnectionStrings = configuration[connectstring];


        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        [ActionName("Project")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult GetProjects(int Id_Project)
        {
            List<ProjectModel> result = new List<ProjectModel>();
            IActionResult response = Unauthorized();
            try
            {
                ProjectModel project = new ProjectModel();
                project.projectID = Id_Project;
                result = _projectService.GetProject(project);

                if (result == null || result.Count == 0)
                {
                    return NotFound(new
                    {
                        Message = "No project found"

                    });
                }
                return Ok(new
                {
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ResponseCode = "500",
                    ResponseMessage = "An error occurred while displaying the project.",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete]
        [ActionName("Project")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]

        public IActionResult DeleteProject(int id_project)
        {
            try
            {
                var result = _projectService.DeleteProjects(id_project);
                return Ok(new
                {
                    ResponseMessage = result.ResponseMessage,
                    Result = result
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    ResponseCode = "500",
                    ResponseMessage = "An error occurred while deleting the project.",
                    Error = ex.Message
                });
            }
        }


        //[HttpGet("by-code/{projectCode}")]
        //public async Task<ActionResult<ProjectModel>> GetProjectByCode(string projectCode)
        //{
        //    try
        //    {
        //        var project = await _projectService.GetProjectByCodeAsync(projectCode);
        //        if (project == null)
        //        {
        //            return NotFound($"Project with code {projectCode} not found.");
        //        }
        //        return Ok(project);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //[HttpGet("by-customer/{customerId}")]
        //public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjectsByCustomer(long customerId)
        //{
        //    try
        //    {
        //        var projects = await _projectService.GetProjectsByCustomerAsync(customerId);
        //        return Ok(projects);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //[HttpGet("by-status/{status}")]
        //public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjectsByStatus(string status)
        //{
        //    try
        //    {
        //        var projects = await _projectService.GetProjectsByStatusAsync(status);
        //        return Ok(projects);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult<long>> CreateProject([FromBody] ProjectModel project)
        //{
        //    try
        //    {
        //        if (project == null)
        //        {
        //            return BadRequest("Project data is required.");
        //        }

        //        var projectId = await _projectService.CreateProjectAsync(project);
        //        return CreatedAtAction(nameof(GetProject), new { id = projectId }, projectId);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateProject(long id, [FromBody] ProjectModel project)
        //{
        //    try
        //    {
        //        if (project == null)
        //        {
        //            return BadRequest("Project data is required.");
        //        }

        //        if (id != project.ID_Project)
        //        {
        //            return BadRequest("ID mismatch.");
        //        }

        //        var result = await _projectService.UpdateProjectAsync(project);
        //        if (!result)
        //        {
        //            return NotFound($"Project with ID {id} not found.");
        //        }

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteProject(long id)
        //{
        //    try
        //    {
        //        var result = await _projectService.DeleteProjectAsync(id);
        //        if (!result)
        //        {
        //            return NotFound($"Project with ID {id} not found.");
        //        }

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
    }
}
