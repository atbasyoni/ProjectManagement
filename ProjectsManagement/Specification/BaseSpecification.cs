using Microsoft.EntityFrameworkCore.Query;
using ProjectsManagement.Models;
using System.Linq.Expressions;

namespace ProjectsManagement.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseModel
    {
        public BaseSpecification()
        {

        }
        public Expression<Func<T, bool>> Criteria { get ; set; }
        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; set; } = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get ; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 0;
        public bool IsPaginationEnabled { get; set; } = false;

        public BaseSpecification(Expression<Func<T, bool>> CriteriaExpression)
        {

            Criteria = CriteriaExpression;

        }

        public void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {

            OrderBy = OrderByExpression;

        }

        public void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExpression)
        {

            OrderByDesc = OrderByDescExpression;

        }
        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;

        }
    }
}
