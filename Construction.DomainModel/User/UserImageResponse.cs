using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.DomainModel.User
{
    public class UserImageResponse : HttpResponses
    {
        public string? ImageUrl { get; set; }
        public string? FileDirectory { get; set; }
        public string? FilePath { get; set; }

        public UserImageResponse()
        {
            ImageUrl = string.Empty;
            FileDirectory = string.Empty;
            FilePath = string.Empty;
        }
    }
}