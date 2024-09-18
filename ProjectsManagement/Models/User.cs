namespace ProjectsManagement.Models
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? OTP { get; set; }
        public DateTime? OTPExpiration { get; set; }
        public bool IsVerified { get; set; } = false;

        public int PictureID { get; set; }
        public Picture Picture { get; set; }

        public ICollection<Project> OwnedProjects { get; set; }
        public ICollection<Project> ProjectMemberships { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<TaskUser> TaskUsers { get; set; }
        public ICollection<PasswordChangeRequest> PasswordChangeRequests { get; set;}
    }
}
