using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Base
{
    public abstract class BaseRepository<T,TContext> : IBaseRepository<T> where T : class where TContext : ApplicationDbContext
    {
        private protected readonly TContext _context;
        
        public BaseRepository(TContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetById(Guid Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity); 
            return entity;
        }
        public T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public async Task<T> Delete(Guid Id)
        {
            var entity = await _context.Set<T>().FindAsync(Id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
            return entity;
        }
    }
}
