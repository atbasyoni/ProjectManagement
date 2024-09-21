using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.Models;

namespace ProjectsManagement.Specification.TaskSpec
{
    public class CountTaskWithSpec:BaseSpecification<Tasks>
    {
         public CountTaskWithSpec(SpecParams specParams)
            :base(p => !p.IsDeleted)
         {

         }
    }
}
