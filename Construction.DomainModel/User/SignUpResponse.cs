using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.User
{
    public class SignUpResponse: HttpResponses
    {
        public string? RegID { get; set; }
        public string? Password { get; set; }


        public SignUpResponse()
        {
            RegID = string.Empty;
            Password = string.Empty;
        }
    }
}
