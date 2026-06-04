using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DAL.Repos
{
    public sealed class Repo<T> : IRepo<T> where T : Entity
    {
        private readonly HybridCache _cache;
        protected readonly WebStoreDBContext _context;
        private readonly DbSet<T> _dbSet;
        public Repo(WebStoreDBContext context, HybridCache cache) {
            _cache = cache;
            _dbSet = context.Set<T>();
            _context = context;
        }
        public async ValueTask<T?> GetAsync(Guid id)
        {
            return await _cache.GetOrCreateAsync($"{id}", async entry => await _dbSet.FindAsync(id));
        }

        public async ValueTask<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async ValueTask AddAsync(T entity, CancellationToken token)
        {
            await _cache.RemoveAsync($"{entity.Id}");
            await _dbSet.AddAsync(entity, token);
        }

        public async ValueTask<Boolean> UpdateAsync(T entity)
        {
            await _cache.RemoveAsync($"{entity.Id}");
            _dbSet.Update(entity);
            return true;
        }

        public async ValueTask<Boolean> DeleteAsync(T entity)
        {
            await _cache.RemoveAsync($"{entity.Id}");
            _dbSet.Remove(entity);
            return true;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
