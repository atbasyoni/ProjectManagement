namespace ProjectsManagement.Models
{
    public class TaskUser : BaseModel
    {
        public int TaskID { get; set; }
        public Tasks Task { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}
