using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Repository.Specification;
using ProjectsManagement.Models;

namespace ProjectsManagement.Specification.TaskSpec
{
    public class TaskWithSpecifications : BaseSpecification<Tasks>
    {
        public TaskWithSpecifications(SpecParams specParams)
            : base(T =>
                (string.IsNullOrEmpty(specParams.Search) || T.Title.ToLower().Contains(specParams.Search.ToLower())))
        {
            Includes.Add(t => t.Include(t => t.AssignedUsers).ThenInclude(au => au.User));
            Includes.Add(t => t.Include(t => t.Project));
 
            if (!string.IsNullOrEmpty(specParams.Search))
            {
                Criteria = t => t.Title.ToLower().Contains(specParams.Search.ToLower());
            }
 
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort.ToLower())
                {
                    case "title":
                        AddOrderBy(t => t.Title);
                        break;
                    case "createddate":
                        AddOrderBy(t => t.CreatedDate);
                        break;
                    case "duedate":
                        AddOrderBy(t => t.DueDate);
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
