using Construction.API.Controllers;
using Construction.Common;
using Construction.DomainModel;
using Construction.DomainModel.User;
using Construction.Interface;
using Construction.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Construction.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [EnableCors] // Enable CORS for all actions in this controller
    public class ProjectController : BaseController
    {
        private const string connectstring = "ConnectionString:DefaultConnection";

        private readonly IConfiguration _configuration;
        private readonly IProjectService _projectService;
        private readonly ITokenService _tokenService;
        private readonly ILoggerService _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly OTPConfig _otp;
        private readonly string _clientid;
        private readonly string _clientsecret;

        public ProjectController(
            IConfiguration configuration,
            IProjectService projectService,
            ITokenService tokenService,
            ILoggerService loggerService,
            IWebHostEnvironment environment,
            IOptions<OTPConfig> otp
        ) : base(configuration)
        {
            _configuration = configuration;
            _projectService = projectService;
            _tokenService = tokenService;
            _logger = loggerService;
            _environment = environment;

            _projectService.ConnectionStrings = configuration[connectstring];
            _logger.ConnectionStrings = configuration[connectstring];

            _clientid = configuration["ClientID"];
            _clientsecret = configuration["ClientSecret"];
            _otp = configuration.GetSection("OTPConfig").Get<OTPConfig>();
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
