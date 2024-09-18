using ProjectsManagement.Enums;

namespace ProjectsManagement.Models
{
    public class Tasks : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public TasksStatus TaskStatus { get; set; }

        public int BoardID { get; set; }
        public Board Board { get; set; }

        public ICollection<TaskUser> AssignedUsers { get; set; }
    }
}
