using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.DomainModel.Common;

namespace Construction.DomainModel
{
    public class PasswordModel: BaseModel
    {
        public Int64 ID_Users { get; set; }       
        public string? UserPassword { get; set; }       
        public string? Email { get; set; }
        public string? OldPassword { get; set; }
        public string? UserName { get; set; }
        public string? UserPasswordStr { get; set; }
        public string? OldPasswordStr { get; set; }
    }
}
