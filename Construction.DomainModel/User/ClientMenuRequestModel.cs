using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.User
{
    public class ClientMenuRequestModel
    {
        public string JsonData { get; set; } = string.Empty;
        public int FK_Tenant { get; set; }
    }
}
