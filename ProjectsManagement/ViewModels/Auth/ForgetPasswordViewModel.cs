using System.ComponentModel.DataAnnotations;

namespace ProjectsManagement.ViewModels.Auth
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
