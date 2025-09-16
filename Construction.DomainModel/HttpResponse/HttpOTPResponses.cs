using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.HttpResponse
{
    public class HttpOTPResponses:HttpResponses
    {
        public string Data { get; set; }

        public HttpOTPResponses()
        {
            Data = string.Empty;
        }
    }
}
