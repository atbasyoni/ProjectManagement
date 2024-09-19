using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Data;
using ProjectsManagement.Models;
using System.Linq.Expressions;


namespace ProjectsManagement.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }
        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _context.Set<T>().Where(a => a.IsDeleted != true);
            return query;
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(a => a.IsDeleted != true && a.ID == id);
        }

        public IQueryable<T> GetAllPagination(int pageNumber, int pageSize)
        {
            var query = _context.Set<T>()
                .Where(a => a.IsDeleted != true)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return query;
        }

        public async Task DeleteAsync(int id)
        {
            T entity = await _context.FindAsync<T>(id);
            await DeleteAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return await Task.FromResult(entity);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> FirstAsyncWithTracking(Expression<Func<T, bool>> predicate)
        {
            return await GetAll(predicate).AsTracking().FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll(predicate).CountAsync();
        }
    }
}
