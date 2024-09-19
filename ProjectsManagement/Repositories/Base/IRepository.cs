using ProjectsManagement.Models;
using System.Linq.Expressions;


namespace ProjectsManagement.Repositories.Base
{
    public interface IRepository<T> where T : BaseModel
    {
        IQueryable<T> GetAll();
        Task<T> GetByIDAsync(int id);
        IQueryable<T> GetAllPagination(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstAsyncWithTracking(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
