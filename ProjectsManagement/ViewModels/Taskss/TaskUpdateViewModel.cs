using System.ComponentModel.DataAnnotations;

namespace ProjectsManagement.ViewModels.Taskss
{
    public class TaskUpdateViewModel
    {
        [Required]
        public int TaskID { get; set; }
        public string Title { get; set;}
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
