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

namespace JWTAuthentication.NET6._0.Controllers
{
    [Route("v1/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        private const string connectstring = "ConnectionString:DefaultConnection";

        private readonly IConfiguration _configuration;
        private readonly IUserService _authService;
        private readonly ITokenService _tokenService;
        private readonly ILoggerService _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly OTPConfig _otp;
        private readonly string _clientid;
        private readonly string _clientsecret;

        public UserController(
            IConfiguration configuration,
            IUserService authService,
            ITokenService tokenService,
            ILoggerService loggerService,
            IWebHostEnvironment environment,
            IOptions<OTPConfig> otp
        ) : base(configuration)
        {
            _configuration = configuration;
            _authService = authService;
            _tokenService = tokenService;
            _logger = loggerService;
            _environment = environment;

            _authService.ConnectionStrings = configuration[connectstring];
            _logger.ConnectionStrings = configuration[connectstring];

            _clientid = configuration["ClientID"];
            _clientsecret = configuration["ClientSecret"];
            _otp = configuration.GetSection("OTPConfig").Get<OTPConfig>();
        }

        #region Authentication

        [HttpPost]
        [ActionName("login")]
        [EnableCors("AllowOrigin")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = new HttpLoginResponse();
            IActionResult response = Unauthorized();
            JwtSecurityToken token = new JwtSecurityToken();
            UsersModel user = new UsersModel();

            try
            {
                if (model == null)
                    return BadRequest(new { Result = "Invalid login request." });

                model.IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

                UsersModel userModel = _authService.ValidateLogin(model);

                if (userModel != null && userModel.ID_Users > 0)
                {
                    token = _tokenService.GenerateToekn(userModel);
                    result.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    result.Expiration = token.ValidTo;
                    result.Sid = Cryptography.EncryptSID(userModel.ID_Users.ToString() ?? "");
                    userModel.FK_UserRoleStr = Cryptography.Encryptstring(userModel.FK_UserRole.ToString());
                    result.ResponseStatus = true;
                    result.ResponseID = 1;
                    result.ResponseCode = "1";
                    result.Response = userModel;
                    result.MenuJson = userModel.MenuJson;

                    response = Ok(new { Result = result });
                }
                else
                {
                    result.ResponseMessage = userModel?.MessageText ?? "Invalid username or password.";
                    result.ResponseStatus = false;
                    result.ResponseID = 0;
                    result.ResponseCode = "0";
                    result.Response = userModel;

                    response = Ok(new { Result = result });
                }
            }
            catch (Exception ex)
            {
                result.ResponseMessage = "Error while validating login.";
                result.ResponseStatus = false;
                response = BadRequest(new { Result = result, Error = ex.Message });
            }

            return await Task.FromResult(response);
        }

        #endregion

        #region Users

        [HttpPost]
        [EnableCors("AllowOrigin")]
        [ActionName("AddUser")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> UsersUpdate([FromBody] UsersModel model)
        {
            SignUpResponse result = new SignUpResponse();
            try
            {
                bool isNewUser = model.ID_Users == 0;
                model.IPAddress = base.IPAddress;
                model.MACAddress = base.MACAddress;
                result = _authService.UsersUpdate(model);

                if (result.ResponseStatus && result.ResponseID > 0 && isNewUser)
                {
                    model.RegistrationID = result.RegID;
                }

                return Task.FromResult<IActionResult>(Ok(new { Result = result }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UsersUpdate");
                return Task.FromResult<IActionResult>(BadRequest(new { Result = result }));
            }
        }

        [HttpPost]
        [EnableCors("AllowOrigin")]
        [ActionName("updateUserDetails")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> UserDataUpdate([FromBody] UsersModel inputModel)
        {
            HttpResponses result = new HttpResponses();
            try
            {
                inputModel.IPAddress = string.IsNullOrEmpty(base.IPAddress) ? "Unknown" : base.IPAddress;
                inputModel.MACAddress = string.IsNullOrEmpty(base.MACAddress) ? "Unknown" : base.MACAddress;

                result = _authService.UserDataUpdate(inputModel);

                bool isUpdate = inputModel.ID_UserProfile > 0;
                if (result.ResponseStatus && result.ResponseID > 0 && isUpdate)
                {
                    return Task.FromResult<IActionResult>(Ok(new
                    {
                        Message = "User details updated successfully",
                        Result = result
                    }));
                }
                else if (result.ResponseStatus && result.ResponseID == 0 && !isUpdate)
                {
                    return Task.FromResult<IActionResult>(Ok(new
                    {
                        Message = "User details inserted successfully",
                        Result = result
                    }));
                }

                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    Message = "Failed to update user details",
                    Result = result
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user details");
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    Message = "An error occurred while updating user details",
                    Error = ex.Message,
                    StackTrace = ex.StackTrace,
                    Result = result
                }));
            }
        }

        [HttpDelete]
        [EnableCors("AllowOrigin")]
        [ActionName("User")]
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> UsersDelete(long id_Users, long createdBy)
        {
            HttpResponses result = new HttpResponses();
            UsersModel model = new UsersModel();
            try
            {
                model.IPAddress = base.IPAddress;
                model.MACAddress = base.MACAddress;
                model.ID_Users = id_Users;
                model.CreatedBy = createdBy;
                result = _authService.UsersDelete(model);
                return Task.FromResult<IActionResult>(Ok(new { Result = result }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UsersDelete");
                return Task.FromResult<IActionResult>(BadRequest(new { Result = result }));
            }
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        [ActionName("User")]
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> UsersSelect(long id_Users)
        {
            List<UsersModel> result = new List<UsersModel>();
            UsersModel model = new UsersModel();
            try
            {
                model.IPAddress = base.IPAddress;
                model.MACAddress = base.MACAddress;
                model.ID_Users = id_Users;
                result = _authService.UsersSelect(model);
                return Task.FromResult<IActionResult>(Ok(new { Result = result }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UsersSelect");
                return Task.FromResult<IActionResult>(BadRequest(new { Result = result }));
            }
        }

        #endregion

        #region DropDowns

        [HttpGet]
        [ActionName("DropDownData")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> DropDownData()
        {
            HttpResponses result = new HttpResponses();
            try
            {
                result = _authService.DropDownData();
                return Task.FromResult<IActionResult>(Ok(new { Result = result }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<IActionResult>(Ok(new { Result = 0, Message = "Error", Error = ex.Message }));
            }
        }

        [HttpGet]
        [ActionName("CommonDropDownData")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> CommonDropDownData()
        {
            HttpResponses result = new HttpResponses();
            try
            {
                result = _authService.CommonDropDownData();
                return Task.FromResult<IActionResult>(Ok(new { Result = result }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<IActionResult>(Ok(new { Result = 0, Message = "Error", Error = ex.Message }));
            }
        }

        #endregion

        #region Menu & Roles

        [HttpPost]
        [ActionName("updatemenu")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> PostMenuModel([FromBody] MenuModel menu)
        {
            HttpResponses result = new HttpResponses();
            try
            {
                result = _authService.PostMenuModel(menu);
                return Task.FromResult<IActionResult>(Ok(new { Result = result }));
            }
            catch (Exception e)
            {
                return Task.FromResult<IActionResult>(BadRequest(new { Result = result, Error = e.Message }));
            }
        }

        [HttpGet]
        [ActionName("getMenu")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> GetMenuModel(int ID_Menu = 0)
        {
            List<MenuModel> result = new List<MenuModel>();
            MenuModel menu = new MenuModel();
            try
            {
                menu.ID_Menu = ID_Menu;
                result = _authService.GetMenuModel(menu);
                return Task.FromResult<IActionResult>(Ok(new { Result = result }));
            }
            catch (Exception e)
            {
                return Task.FromResult<IActionResult>(BadRequest(new { Result = result }));
            }
        }

        [HttpDelete]
        [EnableCors("AllowOrigin")]
        [ActionName("menuDelete")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> MenuDelete(int ID_Menu)
        {
            HttpResponses result = new HttpResponses();
            MenuModel model = new MenuModel();
            try
            {
                model.ID_Menu = ID_Menu;
                result = _authService.MenuDelete(model);
                return Task.FromResult<IActionResult>(Ok(new { Result = result }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "menuDelete");
                return Task.FromResult<IActionResult>(BadRequest(new { Result = result, Error = ex.Message }));
            }
        }

        [HttpPost]
        [EnableCors("AllowOrigin")]
        [ActionName("role")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult RoleModelUpdate(RoleModel userRole)
        {
            var result = _authService.RoleModelUpdate(userRole);
            return Ok(result);
        }

        [HttpGet]
        [ActionName("GetRoles")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult GetRoles(int idRole)
        {
            try
            {
                var result = _authService.GetRoles(idRole);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching roles", details = ex.Message });
            }
        }

        #endregion
    }
}
