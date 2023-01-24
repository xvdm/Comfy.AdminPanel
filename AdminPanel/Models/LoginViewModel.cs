using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
