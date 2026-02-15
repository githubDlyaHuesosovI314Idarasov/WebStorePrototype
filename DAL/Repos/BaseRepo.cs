using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DAL.Repos
{
    public class BaseRepo<Context, T> : IRepo<T> where T : Entity
        where Context : DbContext
    {
        protected readonly Context _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepo(Context context) {
            _dbSet = context.Set<T>();
        }
        public async Task<T> GetAsync(Guid id)
        {
            return (await _dbSet.FindAsync(id))!;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
