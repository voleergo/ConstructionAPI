using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.DomainModel.Common;

namespace Construction.DomainModel
{
    public class PasswordModel : BaseModel
    {
        public string UserInput { get; set; }       
        public string UserPasswordStr { get; set; } 
        public string IPAddress { get; set; }
        public string MACAddress { get; set; }
    }
}
