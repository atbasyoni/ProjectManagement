using System.ComponentModel.DataAnnotations;

namespace ProjectsManagement.ViewModels.Projects
{
    public class AssignUserProjectViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}
