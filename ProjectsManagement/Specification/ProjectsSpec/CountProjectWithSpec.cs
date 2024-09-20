using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.Models;
using ProjectsManagement.Specification;

namespace ProjectManagementSystem.Repository.Specification.ProjectSpecifications
{
    public class CountProjectWithSpec :BaseSpecification<Project>
    {
        public CountProjectWithSpec(SpecParams specParams)
            :base(p => !p.IsDeleted)
        {
            
        }
    }
}
