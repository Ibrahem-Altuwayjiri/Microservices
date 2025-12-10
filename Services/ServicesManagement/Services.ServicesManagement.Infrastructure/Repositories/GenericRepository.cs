using Microsoft.EntityFrameworkCore;
using Services.ServicesManagement.Domain.DBContext;
using Services.ServicesManagement.Domain.IRepositories;
using Services.ServicesManagement.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ServicesManagementDbContext _context;
        protected DbSet<T> dbSet;

        public GenericRepository(
            ServicesManagementDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public async Task<T> Add(T entity)
        {
            await dbSet.AddAsync(entity);
           // await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<List<T>> AddRange(List<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
           // await _context.SaveChangesAsync();
            return entities;
        }
        public async Task<T> FindOneOrDefault(Expression<Func<T, bool>> expression) => await dbSet.FirstOrDefaultAsync(expression);
        public async Task<T?> FindOneOrDefaultWithInclude(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            // Add includes
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(expression);
        }
        public async Task<bool> IsExists(Expression<Func<T, bool>> expression) => await dbSet.AnyAsync(expression);
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression, PaginationParameters? pagination = null)
        {
            if (pagination != null)
            {
                return await dbSet.Where(expression)
                                  .Skip(pagination.PageIndex * pagination.PageSize)
                                  .Take(pagination.PageSize)
                                  .ToListAsync();
            }
            return await dbSet.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindWithInclude(Expression<Func<T, bool>> expression, PaginationParameters? pagination = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            // Add includes
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            query = query.Where(expression);

            if (pagination != null)
            {
                query = query.Skip(pagination.PageIndex * pagination.PageSize)
                                  .Take(pagination.PageSize);
            }

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<T>> FindWithInclude(
                                                Expression<Func<T, bool>> expression,
                                                PaginationParameters? pagination = null,
                                                Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = dbSet;

            if (include != null)
                query = include(query);   // This enables Include + ThenInclude

            query = query.Where(expression);

            if (pagination != null)
            {
                query = query
                    .Skip(pagination.PageIndex * pagination.PageSize)
                    .Take(pagination.PageSize);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAll(PaginationParameters pagination)
        {
            return await dbSet.Skip(pagination.PageIndex * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
        }
        public async Task<T> GetById(int id) => await dbSet.FindAsync(id);

        public async Task<T> Remove(int id)
        {
            var entity = await dbSet.FindAsync(id);

            if (entity != null)
            {
                dbSet.Remove(entity);
                //await _context.SaveChangesAsync();
            }

            return entity;
        }

        public async Task<T> Remove(T model)
        {
            var key = _context.Entry(model).Metadata.FindPrimaryKey();
            var keyValues = key.Properties
                    .Select(p => p.PropertyInfo!.GetValue(model))
                    .ToArray();
            var entity = await dbSet.FindAsync(keyValues);

            if (entity != null)
            {
                dbSet.Remove(entity);
                //await _context.SaveChangesAsync();
            }

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            dbSet.Update(entity);

            //await _context.SaveChangesAsync();

            return entity;
        }
        public async Task<int> Count()
        {
            return await dbSet.CountAsync();
        }

        public async Task<int> Count(Expression<Func<T, bool>> expression)
        {
            return await dbSet.CountAsync(expression);
        }

    }
}
