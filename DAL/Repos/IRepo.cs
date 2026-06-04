using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public interface IRepo<T> where T : class
    {
        public ValueTask<T?> GetAsync(Guid id);
        public ValueTask<IEnumerable<T>> GetAllAsync();
        public ValueTask AddAsync(T entity, CancellationToken token);
        public ValueTask<Boolean> UpdateAsync(T entity);
        public ValueTask<Boolean> DeleteAsync(T entity);
    }
}
