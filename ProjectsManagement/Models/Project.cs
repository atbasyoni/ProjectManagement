namespace ProjectsManagement.Models
{
    public class Project : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Tasks> Tasks { get; set; }
        public List<UserProject> UserProjects { get; set; }
    }
}
