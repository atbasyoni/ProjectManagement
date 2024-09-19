using ProjectsManagement.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsManagement.Models
{
    public class Project : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectStatus ProjectStatus { get; set; }

        public int? OwnerID { get; set; }
        public User Owner { get; set; }

        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
    }
}
