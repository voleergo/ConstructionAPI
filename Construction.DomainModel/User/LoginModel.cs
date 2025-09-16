using System.ComponentModel.DataAnnotations;
using Construction.DomainModel.Common;

namespace Construction.DomainModel
{
    public class LoginModel:BaseModel
    {       
        public  string? Username { get; set; }      
        public  string? Password { get; set; }
        public string? MobileNumber { get; set; }

        
        public int ID_PartyMember { get; set; }


    }
}
