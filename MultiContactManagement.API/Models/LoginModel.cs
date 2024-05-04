using System.ComponentModel.DataAnnotations;

namespace MultiContactManagement.API.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is mandatory.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is mandatory.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
