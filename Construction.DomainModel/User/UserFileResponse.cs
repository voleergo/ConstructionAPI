using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.User
{
    public class UserFileResponse : HttpResponses
    {
        public string? FileUrl { get; set; }
        public string? FileDirectory { get; set; }
        public string? FilePath { get; set; }

        public UserFileResponse()
        {
            FileUrl = string.Empty;
            FileDirectory = string.Empty;
            FilePath = string.Empty;
        }
    }
}