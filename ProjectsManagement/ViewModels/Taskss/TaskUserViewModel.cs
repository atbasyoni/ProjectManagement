using System.ComponentModel.DataAnnotations;

namespace ProjectsManagement.ViewModels.Taskss
{
    public class TaskUserViewModel
    {
        [Required]
        public int taskID { get; set; }
        [Required]
        public int userID { get; set; }
    }
}
