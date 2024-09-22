using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.Models;

namespace ProjectsManagement.Specification.UserSpecifications
{
    public class UserWithSpecifications:BaseSpecification<User>
    {
        public UserWithSpecifications(SpecParams specParams)
            : base(T =>
                (string.IsNullOrEmpty(specParams.Search) || T.UserName.ToLower().Contains(specParams.Search.ToLower())))
        {
            if (!string.IsNullOrEmpty(specParams.Search))
            {
                Criteria = t => t.UserName.ToLower().Contains(specParams.Search.ToLower());
            }

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort.ToLower())
                {
                    case "title":
                        AddOrderBy(t => t.UserName);
                        break;
                    case "createddate":
                        AddOrderBy(t => t.CreatedDate);
                        break;
                    default:
                        AddOrderBy(t => t.CreatedDate);
                        break;
                }
            }

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);

        }
    }
}
