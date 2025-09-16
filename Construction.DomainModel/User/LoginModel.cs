using System.ComponentModel.DataAnnotations;
using Construction.DomainModel.Common;

namespace Construction.DomainModel
{
    public class LoginModel : BaseModel
    {
        [Required(ErrorMessage = "User input is required (Email or Mobile).")]
        public string? UserInput { get; set; }   // Maps to @UserInput (Email or Mobile)

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }    // Maps to @Password
    }
}
