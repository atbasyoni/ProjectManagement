namespace ProjectsManagement.Models
{
    public class Tasks : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus TaskStatus { get; set; }

        public int ProjectID { get; set; }
        public Project Project { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}
