namespace ProjectsManagement.Models
{
    public class Board : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int ProjectID { get; set; }
        public Project Project { get; set; }

        public ICollection<Tasks> Tasks { get; set; }
    }
}
