using System.ComponentModel.DataAnnotations;

namespace GlobalChat.Models
{
    public class LogInModel
    {
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}