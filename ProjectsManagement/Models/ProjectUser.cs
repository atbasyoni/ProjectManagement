namespace ProjectsManagement.Models
{
    public class ProjectUser : BaseModel
    {
        public int ProjectID { get; set; }
        public Project Project { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}
