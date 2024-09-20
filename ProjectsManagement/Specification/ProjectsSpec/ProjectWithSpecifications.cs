using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.Models;

namespace ProjectsManagement.Specification.ProjectsSpec
{
    public class ProjectWithSpecifications : BaseSpecification<Project>
    {
        public ProjectWithSpecifications(SpecParams specParams)
            : base(P =>
                (string.IsNullOrEmpty(specParams.Search) || P.Title.ToLower().Contains(specParams.Search.ToLower())))
        {
            Includes.Add(p => p.Include(p => p.Tasks));
            Includes.Add(p => p.Include(p => p.ProjectUsers).ThenInclude(up => up.User));

            if (!string.IsNullOrEmpty(specParams.Search))
            {
                Criteria = p => p.Title.ToLower().Contains(specParams.Search.ToLower());
            }

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort.ToLower())
                {
                    case "title":
                        AddOrderBy(p => p.Title);
                        break;
                    default:
                        AddOrderBy(p => p.CreatedDate);
                        break;
                }
            }

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }

    }
}
