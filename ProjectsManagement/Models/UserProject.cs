namespace ProjectsManagement.Models
{
    public class UserProject
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int ProjectID { get; set; }
        public Project Project { get; set; }
    }
}
