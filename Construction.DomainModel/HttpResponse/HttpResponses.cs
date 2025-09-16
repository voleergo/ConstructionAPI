using System;
using System.Collections.Generic;
using System.Text;

namespace Construction.DomainModel
{
    public class HttpResponses
    {       
        public string? ResponseCode { get; set; }      
        public bool ResponseStatus { get; set; }      
        public object? Response { get; set; }       
        public string? ResponseType { get; set; }      
        public string? ResponseMessage { get; set; }
        public Int64 ResponseID { get; set; }
        public string UniqueID { get; set; }
        public string ReferenceNumber { get; set; }
        public string ResponseData { get; set; }
        public  string MenuJson { get; set; }


        public HttpResponses()
        {
            ResponseData = string.Empty;
            ResponseCode = "0";
            ResponseStatus = false;
            ResponseType = string.Empty;
            ResponseMessage = string.Empty;
            ResponseID = 0;
            UniqueID = string.Empty;
        }
    }
}
