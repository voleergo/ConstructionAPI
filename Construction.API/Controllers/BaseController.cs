using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Construction.Common;

namespace Construction.API.Controllers
{
    [Route("v1/[action]")]
    [ApiController]
    [EnableCors("ProductionPolicy")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BaseController : ControllerBase, IActionFilter
    {
        private readonly IConfiguration _configuration;
        public string IPAddress { get; set; }
        public string MACAddress { get; set; }
        public Int64 ID_Users { get; set; }

        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
            IPAddress = string.Empty;
            MACAddress = string.Empty;
            ID_Users = 0;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var aa = "";
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            IPAddress = Convert.ToString(Request.HttpContext.Connection.RemoteIpAddress);
            MACAddress = "mac"; //GetClientMAC(IPAddress);
            string token = string.Empty, sid = string.Empty;
            const string BearerPrefix = "Bearer ";
            var actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
            var controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
            string[] excludeMethod = { "login", "AdminLogin", "DropDownData", "GetReportData", "DepartmentDetails", "Unauthorized", "authorize", "SectionSelect", "signup", "images", "DoctorSelect", "DropDownSelect", "DropDownData", "nominationDocuments", "fillcompanydropdown", "forgotpassword", "announcementglobal", "usermanual", "eventresult", "eventresultapproved", "eventresultgoroupbycompany", "eventresulttopcompany", "eventresultdetails", "UpdateMenuRole" };

            //if (!excludeMethod.Contains(actionName))
            //{
            //if (Request.Headers.TryGetValue("token", out StringValues sidValue))
            //{
            //if (string.IsNullOrEmpty(sidValue))
            //{
            //    context.Result = new RedirectResult("/v1/Unauthorized");
            //}
            //else
            //{
            //    sid = Convert.ToString(sidValue);
            //    if (Request.Headers.TryGetValue("Authorization", out StringValues headerValue))
            //    {
            //        token = Convert.ToString(headerValue);
            //        if (!string.IsNullOrEmpty(token) && token.StartsWith(BearerPrefix))
            //        {
            //            token = token.Substring(BearerPrefix.Length);
            //            var handler = new JwtSecurityTokenHandler();
            //            var jwtSecurityToken = handler.ReadJwtToken(token);
            //            //if (jwtSecurityToken != null)
            //            //{
            //            //    //var userId = Convert.ToString(jwtSecurityToken.Claims.First(x => x.Type == "sid").Value);
            //            //    //if (userId != sid)
            //            //    //{
            //            //    //    context.Result = new RedirectResult("/v1/Unauthorized");
            //            //    //}
            //            //    else
            //            //    {
            //            //        ID_Users = Convert.ToInt64(Cryptography.DecryptSID(sid));
            //            //    }
            //            //}
            //            //else
            //            //{
            //            //    context.Result = new RedirectResult("/v1/Unauthorized");
            //            //}
            //        }
            //        else
            //        {
            //            context.Result = new RedirectResult("/v1/Unauthorized");
            //        }
            //    }
            //    else
            //    {
            //        context.Result = new RedirectResult("/v1/Unauthorized");
            //    }
            //}
            //}
            //else
            //{
            //    context.Result = new RedirectResult("/v1/Unauthorized");
            //}
            //}
        }

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        private static string GetClientMAC(string strClientIP)
        {
            string mac_dest = "";
            try
            {
                Int32 ldest = inet_addr(strClientIP);
                Int32 lhost = inet_addr("");
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");

                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }

                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }
            }
            catch (Exception err)
            {

            }
            return mac_dest;
        }
    }
}
