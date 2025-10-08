using System.ComponentModel.DataAnnotations;

namespace ClinicaPro.Web.Models
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Lembrar login")]
        public bool RememberMe { get; set; }
    }
}
