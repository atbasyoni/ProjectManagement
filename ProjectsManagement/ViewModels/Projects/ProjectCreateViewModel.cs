using System.ComponentModel.DataAnnotations;

namespace ProjectsManagement.ViewModels.Projects
{
    public class ProjectCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
