using System;
using System.Collections.Generic;
using System.Text;

namespace Construction.DomainModel
{
    public class HttpLoginResponse: HttpResponses
    {  
        public string? Token { get;set; }
        public DateTime? Expiration { get; set; }
        public string? Sid { get; set; }

        public HttpLoginResponse()
        {          
            Token = string.Empty;
            Expiration = DateTime.Now;
            Sid = string.Empty;

        }
    }
}
