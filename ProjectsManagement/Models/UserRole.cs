namespace ProjectsManagement.Models
{
    public class UserRole : BaseModel
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
