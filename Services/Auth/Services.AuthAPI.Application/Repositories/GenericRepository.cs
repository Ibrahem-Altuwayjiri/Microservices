using Microsoft.EntityFrameworkCore;
using Services.AuthAPI.Infrastructure.Data;
using Services.AuthAPI.Infrastructure.IRepositories;
using Services.AuthAPI.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthAPI.Application.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext _context;
        protected DbSet<T> dbSet;

        public GenericRepository(
            AppDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public async Task<T> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> FindOneOrDefault(Expression<Func<T, bool>> expression) => await dbSet.FirstOrDefaultAsync(expression);
        public async Task<bool> IsExists(Expression<Func<T, bool>> expression) => await dbSet.AnyAsync(expression);
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression) => await dbSet.Where(expression).ToListAsync();
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
                await _context.SaveChangesAsync();
            }

            return entity;
        }

        public async Task<T> Update(T entity)
        {
            dbSet.Update(entity);

            await _context.SaveChangesAsync();

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
