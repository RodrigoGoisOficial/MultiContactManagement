using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MultiContactManagement.Application.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-mail is required.")]
        [MaxLength(100, ErrorMessage = "E-mail cannot exceed 100 characters.")]
        public string Email { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public bool IsAdmin { get; set; }
    }
}
