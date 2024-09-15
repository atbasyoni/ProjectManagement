using System.ComponentModel.DataAnnotations;

namespace ProjectsManagement.ViewModels.Auth
{
    public class LoginRequestViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
