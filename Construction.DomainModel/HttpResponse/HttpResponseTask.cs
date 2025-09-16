using System;
using System.Collections.Generic;
using System.Text;
using Construction.DomainModel;

namespace Construction.DomainModel
{
    public class HttpResponseTask : Tasks
    {
        public object Response { get; set; }
        public Int64 ResponseCode { get; set; }
        public string ResponseType { get; set; }
        public string ResponseText { get; set; }
        public DateTime Expiration { get; set; }
        public string Sid { get; set; }

        public HttpResponseTask()
        {
            ResponseCode = 0;
            ResponseType = string.Empty;
            ResponseText = string.Empty;
            Expiration = DateTime.Now;
            Sid = string.Empty;

        }
    }
}
