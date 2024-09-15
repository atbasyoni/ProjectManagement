using System.ComponentModel.DataAnnotations;

namespace ProjectsManagement.ViewModels.Auth
{
    public class RegisterRequestViewModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, and include at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Confirm Password does not match the Password.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
