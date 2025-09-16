using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.User
{
    public class OTPConfig
    {
        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }
        public string SenderName { get; set; }
        public int RouteType { get; set; }
        public string TemplateId { get; set; }
    }
}
