using DAL;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebStorePrototype.Server.Features.Base;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Features.CrudHandlers
{
    public class CreateEntityHandler<T> : IRequestHandler<CreateCommand<T>, T> where T : Entity
    {
        private readonly BaseRepo<DbContext, T> _repo;
        private readonly RedisService<T> _redis;
        private readonly string _cachePrefix;

        public CreateEntityHandler(DbContext context, RedisService<T> redis)
        {
            _repo = new BaseRepo<DbContext, T>(context);
            _redis = redis;
            _cachePrefix = typeof(T).Name.ToLower();
        }

        public async Task<T> Handle(CreateCommand<T> request, CancellationToken cancellationToken)
        {
            await _repo.AddAsync(request.Entity);
            await _repo.SaveAsync();

            await _redis.SetAsync($"{_cachePrefix}:{request.Entity.Id}", request.Entity);
            await _redis.DeleteAsync($"{_cachePrefix}s:all");

            return request.Entity;
        }
    }
}
