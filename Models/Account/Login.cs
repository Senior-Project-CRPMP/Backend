using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Account
{
    public class Login
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
