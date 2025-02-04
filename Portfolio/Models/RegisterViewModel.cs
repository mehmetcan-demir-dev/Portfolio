using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Eposta veya Şifre Yanlış")]
        public string ConfirmPassword { get; set; }
    }
}
