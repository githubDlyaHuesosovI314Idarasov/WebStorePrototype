using DAL;
using DAL.EF;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using WebStorePrototype.Server.Features.Base;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Features.CrudHandlers
{
    public class CreateEntityHandler<T> : IRequestHandler<CreateCommand<T>, T> where T : Entity
    {
        private readonly TimeSpan _timeSpan = TimeSpan.FromMinutes(5);
        private readonly Repo<T> _repo;
        private readonly RedisService<T> _redis;
        private readonly string _cachePrefix;

        public CreateEntityHandler(WebStoreDBContext context, RedisService<T> redis, HybridCache cache)
        {
            _repo = new Repo<T>(context, cache);
            _redis = redis;
            _cachePrefix = typeof(T).Name.ToLower();
        }

        public async Task<T> Handle(CreateCommand<T> request, CancellationToken cancellationToken)
        {
            await _repo.AddAsync(request.Entity, cancellationToken);
            await _repo.SaveAsync();

            await _redis.SetAsync($"{_cachePrefix}:{request.Entity.Id}", request.Entity, _timeSpan);
            await _redis.DeleteAsync($"{_cachePrefix}s:all");

            return request.Entity;
        }
    }
}
