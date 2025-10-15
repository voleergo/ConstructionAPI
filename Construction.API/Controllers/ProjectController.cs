using Construction.DomainModel;
using Construction.DomainModel.Item;
using Construction.DomainModel.Project;
using Construction.DomainModel.User;
using Construction.Interface;
using Construction.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Construction.API.Controllers
{
    [Route("v1/[action]")]
    [ApiController]
    [EnableCors("ProductionPolicy")]
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
        //[Authorize]
        [ActionName("Project")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult GetProjects(int Id_Project = 0, int userId = 0) // Default to 28
        {
            try
            {
                var project = new ProjectModel
                {
                    projectID = Id_Project,
                    FK_User = userId
                };

                var result = _projectService.GetProject(project);
                if (result == null || result.Count == 0)
                    return NotFound(new { Message = "No projects found for user ID: " + userId });

                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ResponseCode = "500",
                    ResponseMessage = "An error occurred while retrieving projects.",
                    Error = ex.Message
                });
            }
        }


        [HttpPost]
        [ActionName("Project")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult UpdateProject([FromBody] ProjectModel project)
        {
            try
            {
                if (project == null || string.IsNullOrEmpty(project.json))
                {
                    return BadRequest(new
                    {
                        ResponseCode = 0,
                        ResponseMessage = "Invalid input data",
                        ResponseStatus = false
                    });
                }

                // Get logged-in user ID from claims
                int loggedInUserId = 0;
                if (User?.Identity?.IsAuthenticated == true)
                {
                    var userClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID")
                                    ?? User.Claims.FirstOrDefault(c => c.Type == "nameid"); // fallback
                    if (userClaim != null && int.TryParse(userClaim.Value, out int userId))
                    {
                        loggedInUserId = userId;
                    }
                }

                // Fallback for testing (remove in production)
                if (loggedInUserId == 0)
                {
                    loggedInUserId = 16;
                }

                // Pass FK_User to service
                project.FK_User = loggedInUserId;

                HttpResponses response = _projectService.UpdateProject(project);

                return Ok(new
                {
                    ResponseCode = response.ResponseCode,
                    ResponseMessage = response.ResponseMessage,
                    ResponseStatus = response.ResponseStatus,
                    ResponseProjectID = response.ResponseID
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Project Upsert");
                return BadRequest(new
                {
                    ResponseCode = 0,
                    ResponseMessage = ex.Message,
                    ResponseStatus = false
                });
            }
        }

        [HttpDelete]
        [ActionName("Project")]
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

        [HttpGet]
        [ActionName("ProjectUsers")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult GetProjectUsers(int projectId)
        {
            try
            {
                if (projectId <= 0)
                {
                    return BadRequest(new
                    {
                        ResponseCode = 0,
                        ResponseMessage = "Invalid project ID",
                        ResponseStatus = false
                    });
                }

                // Call service method
                List<ProjectUserModel> result = _projectService.GetProjectUsers(projectId);

                if (result == null || result.Count == 0)
                {
                    return NotFound(new
                    {
                        Message = "No users found for the project"
                    });
                }

                return Ok(new
                {
                    Result = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching project users");
                return StatusCode(500, new
                {
                    ResponseCode = "500",
                    ResponseMessage = "An error occurred while retrieving project users.",
                    Error = ex.Message
                });
            }
        }



    }
}
