/*----------------------------------- UsersDataService  -----------------------------------------------------------------------------------------------------------------------
Purpose    : Auth Controller Class
Author     : Jinesh Kumar C
Copyright  :
Created on :01-11-2023 09:32:03
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS On                          By                    TaskID          Description
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
01-11-2023 09:32:03                      Jinesh Kumar C                       Auth Controller class initially  created
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Construction.DomainModel;
using Construction.Interface;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;
using System.Runtime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;
using Construction.DomainModel;
using Construction.API.Controllers;
using Construction.Common;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Net.Http.Headers;
using System.Reflection;
using System.Net.Sockets;
using Construction.DomainModel;
using Construction.Service;
using System.Net.WebSockets;
using Construction.DomainModel;
using Azure.Core;
using Construction.Interface;
using Microsoft.AspNetCore.Http;
using Construction.API.Controllers;
using Construction.Interface;
using Construction.DomainModel.User;
using Construction.DomainModel;

namespace JWTAuthentication.NET6._0.Controllers
{
    ///[Route("v1/[controller]/[action]")
    [Route("v1/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        private const string connectstring = "ConnectionString";
        private readonly IConfiguration _configuration;
        private IUserService _authService;
        private ITokenService _tokenService;
        private ILoggerService _logger;
        private IWebHostEnvironment _environment;
        private object _userService;
        private readonly string _clientid = string.Empty;
        private readonly string _clientsecret = string.Empty;
        private readonly OTPConfig _otp;


        public UserController(IConfiguration configuration, IUserService authService, ITokenService tokenService, ILoggerService loggerService, IWebHostEnvironment environment, OTPConfig otp) : base(configuration)
        {
            _authService = authService;
            _configuration = configuration;
            _tokenService = tokenService;
            _logger = loggerService;
            _authService.ConnectionStrings = Convert.ToString(_configuration[connectstring]);
            _logger.ConnectionStrings = Convert.ToString(_configuration[connectstring]);
            _environment = environment;

            _clientid = _configuration["CliendID"];
            _clientsecret = _configuration["ClientSecret"];
            _otp = _configuration.GetSection("OTPConfig").Get<OTPConfig>();
        }













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

                // attach extra info
                model.IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                // model.MACAddress = ... (depends how you’re capturing it)

                UsersModel userModel = _authService.ValidateLogin(model);

                if (userModel != null && userModel.ID_Users > 0)
                {
                    // generate token
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
















        #region Users

        [HttpPost]
        [EnableCors("AllowOrigin")]
        [ActionName("AddUser")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> UsersUpdate([FromBody] UsersModel model)
        {
            SignUpResponse result = new SignUpResponse();
            IActionResult response = Unauthorized();
            try
            {
                bool isNewUser = model.ID_Users == 0;
                Int64 id_user = base.ID_Users;
                model.IPAddress = base.IPAddress;
                model.MACAddress = base.MACAddress;
                result = _authService.UsersUpdate(model);

                if (result.ResponseStatus && result.ResponseID > 0 && isNewUser)
                {
                    model.RegistrationID = result.RegID;

                }

                return Task.FromResult<IActionResult>(Ok(new
                {
                    Result = result
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UsersUpdate");
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    Result = result
                }));
            }
            finally
            {

            }
        }
        [HttpPost]
        [EnableCors("AllowOrigin")]
        [ActionName("updateUserDetails")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> UserDataUpdate([FromBody] UsersModel inputModel)
        {
            HttpResponses result = new HttpResponses();
            IActionResult response = Unauthorized();
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
        public Task<IActionResult> UsersDelete(Int64 id_Users, Int64 createdBy)
        {
            HttpResponses result = new HttpResponses();
            IActionResult response = Unauthorized();
            UsersModel model = new UsersModel();
            try
            {
                model.IPAddress = base.IPAddress;
                model.MACAddress = base.MACAddress;
                model.ID_Users = id_Users;
                model.CreatedBy = createdBy;
                result = _authService.UsersDelete(model);
                return Task.FromResult<IActionResult>(Ok(new
                {
                    Result = result
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UsersDelete");
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    Result = result
                }));
            }
            finally
            {
            }
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        [ActionName("User")]
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> UsersSelect(Int64 id_Users, Int64 createdBy)
        {
            List<UsersModel> result = new List<UsersModel>();
            IActionResult response = Unauthorized();
            UsersModel model = new UsersModel();
            try
            {
                model.IPAddress = base.IPAddress;
                model.MACAddress = base.MACAddress;
                model.ID_Users = id_Users;

                // Fix for CS0029: Convert 'createdBy' (long) to string before assignment
                model.CreatedBy = createdBy.ToString();

                result = _authService.UsersSelect(model);
                return Task.FromResult<IActionResult>(Ok(new
                {
                    Result = result
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UsersSelect");
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    Result = result
                }));
            }
            finally
            {

            }
        }


        #endregion Users

        [HttpGet]
        [EnableCors("AllowOrigin")]
        [ActionName("Unauthorized")]
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> InvalidSession()
        {
            HttpResponses result = new HttpResponses();
            IActionResult response = Unauthorized();
            try
            {
                return Task.FromResult<IActionResult>(Unauthorized(new
                {
                    Result = result
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "InvalidSession");
                return Task.FromResult<IActionResult>(Unauthorized(new
                {
                    Result = result
                }));
            }
            finally
            {

            }
        }

        [HttpGet]
        [ActionName("DropDownData")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> DropDownData()
        {
            HttpResponses result = new HttpResponses();
            IActionResult response = Unauthorized();
            try
            {
                result = _authService.DropDownData();
                return Task.FromResult<IActionResult>(Ok(new
                {
                    Result = result
                }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<IActionResult>(Ok(new
                {
                    Result = 0,
                    Message = "An error occurred while processing your request.",
                    Error = ex.Message
                }));
            }
        }
        [HttpGet]
        [ActionName("CommonDropDownData")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> CommonDropDownData()
        {
            HttpResponses result = new HttpResponses();
            IActionResult response = Unauthorized();
            try
            {
                result = _authService.CommonDropDownData();
                return Task.FromResult<IActionResult>(Ok(new
                {
                    Result = result
                }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<IActionResult>(Ok(new
                {
                    Result = 0,
                    Message = "An error occurred while processing your request.",
                    Error = ex.Message
                }));
            }
        }

        [HttpPost]
        [ActionName("generatetoken")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> GenerateToken()
        {
            try
            {
                string token;
                string sid;
                var clientId = Request.Headers["ClientID"].ToString();
                var clientSecret = Request.Headers["ClientSecret"].ToString();

                if (clientId == _clientid && clientSecret == _clientsecret)
                {
                    token = _tokenService.GenerateToken();
                    //sid = Cryptography.EncryptSID(Convert.ToString("123") ?? "");
                    return Task.FromResult<IActionResult>(Ok(new
                    {
                        Message = "Authentication successful",
                        token = token,
                        //sid = sid
                    }));
                }


                return Task.FromResult<IActionResult>(Unauthorized(new
                {
                    Message = "Authentication not successful - invalid credentials"
                }));
            }
            catch (Exception ex)
            {
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    Message = ex.Message
                }));
            }
        }
        [HttpGet]
        [ActionName("GetTenantData")]
        [EnableCors("AllowOrigin")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult GetTenantData()
        {
            IActionResult response = Unauthorized();
            try
            {
                var result = _authService.GetTenantData();  // ✅ now returns string
                return response = Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return response = BadRequest(new { Result = 0, Message = ex.Message });
            }
        }

        [HttpGet]
        [ActionName("menuclient")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> GetMenuClient(int FK_Tenant, int CreatedBy, int ID_MenuClient = 0)
        {
            List<ClientMenuModel> result = new List<ClientMenuModel>();
            IActionResult response = Unauthorized();
            ClientMenuModel model = new ClientMenuModel();
            try
            {
                model.FK_Tenant = FK_Tenant;
                model.CreatedBy = CreatedBy;
                model.ID_MenuClient = ID_MenuClient;

                result = _authService.GetMenuClient(model);

                return Task.FromResult<IActionResult>(Ok(new
                {
                    Result = result
                }));
            }
            catch (Exception e)
            {
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    Result = result,
                    Error = e.Message
                }));
            }
        }
        [HttpPost]
        [ActionName("UpdateMenuClient")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult UpdateMenuClient([FromBody] ClientMenuModel client)
        {
            var result = _authService.UpdateMenuClient(client);
            return Ok(result);
        }


        [HttpGet]
        [ActionName("menurole")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> GetMenuRole(int FK_Role, int FK_Tenant)
        {
            List<MenuRoleModel> result = new List<MenuRoleModel>();
            IActionResult response = Unauthorized();
            MenuRoleModel menu = new MenuRoleModel();
            try
            {
                menu.FK_Role = FK_Role;
                menu.FK_Tenant = FK_Tenant;
                result = _authService.GetMenuRoleModel(menu);
                return Task.FromResult<IActionResult>(Ok(new
                {
                    Result = result
                }));


            }
            catch (Exception e)
            {
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    Result = result
                }));
            }

        }
        [HttpPost]
        [ActionName("UpdateMenuRole")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult UpdateMenuRole([FromBody] MenuRoleModel menu)
        {
            var result = _authService.UpdateMenuRole(menu);
            return Ok(result);
        }




        #region Role
        [HttpPost]
        [EnableCors("AllowOrigin")]
        [ActionName("role")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult RoleModelUpdate(RoleModel userRole)
        {
            var result = _authService.RoleModelUpdate(userRole);
            return Ok(result);
        }


        #endregion Role
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

        //[HttpDelete]
        //[EnableCors]
        //[ActionName("deleteUserrole")]



        //[ApiExplorerSettings(IgnoreApi = false)]
        //public Task<IActionResult> UserRoleDelete(Int32 id)
        //{
        //    HttpResponses result = new HttpResponses();
        //    IActionResult response = Unauthorized();
        //    RoleModel model = new RoleModel();
        //    try
        //    {
        //        model.ID = id;
        //        result = _authService.userRoleDelete(model);
        //        return Task.FromResult<IActionResult>(Ok(new
        //        {
        //            Result = result
        //        }));
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e, "UserRoleDelete");
        //        return Task.FromResult<IActionResult>(BadRequest(new
        //        {
        //            Result = result
        //        }));
        //    }
        //    finally
        //    {
        //    }

        //}

        //[HttpGet]
        //[ActionName("getmenuRole")]
        //[EnableCors]
        //[ApiExplorerSettings(IgnoreApi = false)]
        //public Task<IActionResult> GetMenuRole(int ID)
        //{
        //    List<RoleModel> result = new List<RoleModel>();
        //    IActionResult response = Unauthorized();
        //    RoleModel role = new RoleModel();

        //    try
        //    {
        //        role.ID = ID;
        //        result = _authService.getUserRole(role);
        //        return Task.FromResult<IActionResult>(Ok(new
        //        {
        //            Result = result
        //        }));
        //    }
        //    catch (Exception e)
        //    {
        //        return Task.FromResult<IActionResult>(BadRequest(new
        //        {
        //            Result = result
        //        }));
        //    }



        //}

        [HttpPost]
        [EnableCors("AllowOrigin")]
        [ActionName("updatemenu")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> PostMenuModel([FromBody] MenuModel menu)
        {
            IActionResult response = Unauthorized();
            HttpResponses result = new HttpResponses();
            try
            {
                result = _authService.PostMenuModel(menu);
                return Task.FromResult<IActionResult>(Ok(new
                {
                    Result = result
                }));
            }
            catch (Exception e)
            {
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    Result = result,
                    Error = e.Message
                }));
            }
        }
        [HttpGet]
        [ActionName("getMenu")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> GetMenuModel(int ID_Menu = 0) // Changed parameter name and added default value
        {
            List<MenuModel> result = new List<MenuModel>();
            IActionResult response = Unauthorized();
            MenuModel menu = new MenuModel();
            try
            {
                menu.ID_Menu = ID_Menu; // Changed from FK_Menu to ID_Menu
                result = _authService.GetMenuModel(menu);
                return Task.FromResult<IActionResult>(Ok(new
                {
                    Result = result
                }));
            }
            catch (Exception e)
            {
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    Result = result
                }));
            }
        }
        [HttpDelete]
        [EnableCors("AllowOrigin")]
        [ActionName("menuDelete")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public Task<IActionResult> MenuDelete(Int32 ID_Menu) // Changed parameter name from fK_Menu to ID_Menu
        {
            HttpResponses result = new HttpResponses();
            IActionResult response = Unauthorized();
            MenuModel model = new MenuModel();
            try
            {
                model.ID_Menu = ID_Menu; // Changed from FK_Menu to ID_Menu
                result = _authService.MenuDelete(model);
                return Task.FromResult<IActionResult>(Ok(new
                {
                    Result = result
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "menuDelete");
                return Task.FromResult<IActionResult>(BadRequest(new
                {
                    Result = result,
                    Error = ex.Message
                }));
            }
        }
    }
}



