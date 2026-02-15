using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public interface IRepo<T> where T : class
    {
        public Task<T> GetAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task AddAsync(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
