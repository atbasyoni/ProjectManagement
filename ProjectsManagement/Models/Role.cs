namespace ProjectsManagement.Models
{
    public class Role : BaseModel
    {
        public static int User = 1;
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
