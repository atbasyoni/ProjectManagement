using System.ComponentModel.DataAnnotations;

namespace ProjectsManagement.ViewModels.Auth
{
    public class ChangePasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [RegularExpression(@"^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[@$!%?&])[A-Za-z\d@$!%?&]{8,}$",
        ErrorMessage = "New password must be at least 8 characters long, and include at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        
        public string NewPassword { get; set; }
    }
}
