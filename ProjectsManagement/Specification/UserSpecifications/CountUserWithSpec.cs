using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.Models;

namespace ProjectsManagement.Specification.UserSpecifications
{
    public class CountUserWithSpec:BaseSpecification<User>
    {
        public CountUserWithSpec(SpecParams specParams)
            : base(u => !u.IsDeleted)
        {

        }
    }
}
