namespace ProjectsManagement.Models
{
    public class Project : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public int OwnerID { get; set; }
        public User Owner { get; set; }

        public ICollection<Board> Boards { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
    }
}
