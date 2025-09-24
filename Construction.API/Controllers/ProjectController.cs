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
        
        [HttpPost]
        [EnableCors("AllowOrigin")]
        [ActionName("Project")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult UpdateProject([FromBody] ProjectModel project)
        {
            HttpResponses response = new HttpResponses();
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

                response = _projectService.UpdateProject(project);

                return Ok(new
                {
                    ResponseCode = response.ResponseCode,
                    ResponseMessage = response.ResponseMessage,
                    ResponseStatus = response.ResponseStatus,
                    ResponseProjectID = response.ResponseID,
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

    }
}
