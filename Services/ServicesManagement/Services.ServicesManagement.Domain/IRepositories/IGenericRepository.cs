using Services.ServicesManagement.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Domain.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> GetById(int id);
        public Task<IEnumerable<T>> GetAll(PaginationParameters pagination);
        public Task<IEnumerable<T>> GetAll();
        public Task<int> Count();
        public Task<int> Count(Expression<Func<T, bool>> expression);
        public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression, PaginationParameters? pagination = null);
        public Task<IEnumerable<T>> FindWithInclude(Expression<Func<T, bool>> expression, PaginationParameters? pagination = null, params Expression<Func<T, object>>[] includes);
        public Task<IEnumerable<T>> FindWithInclude(Expression<Func<T, bool>> expression, PaginationParameters? pagination = null, Func<IQueryable<T>, IQueryable<T>>? include = null);
        public Task<T> Update(T entity);
        public Task<T> FindOneOrDefault(Expression<Func<T, bool>> expression);
        public Task<T?> FindOneOrDefaultWithInclude(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        public Task<T> Add(T entity);
        public Task<List<T>> AddRange(List<T> entities);
        public Task<T> Remove(int id);
        public Task<T> Remove(T model);
        public Task<bool> IsExists(Expression<Func<T, bool>> expression);
    }
}
