using DAL;
using DAL.EF;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using StackExchange.Redis;
using WebStorePrototype.Server.Features.Base;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Features.CrudHandlers
{
    public class GetAllEntitiesHandler<T> : IRequestHandler<GetAllQuery<T>, IEnumerable<T>> where T : Entity
    {
        private readonly Repo<T> _repo;
        private readonly RedisService<T> _redis;
        private readonly string _cachePrefix;

        public GetAllEntitiesHandler(WebStoreDBContext context, RedisService<T> redis, HybridCache cache)
        {
            _repo = new Repo<T>(context, cache);
            _redis = redis;
            _cachePrefix = typeof(T).Name.ToLower();
        }

        public async Task<IEnumerable<T>> Handle(GetAllQuery<T> request, CancellationToken cancellationToken)
        {
            RedisKey key = $"{_cachePrefix}s:all"; // "orders:all", "products:all"

            if (await _redis.IsRedisAvailable(key))
            {
                var cached = await _redis.GetListAsync(key);
                if (cached.Any()) return cached;
            }

            var entities = (await _repo.GetAllAsync()).ToList();
            await _redis.SetListAsync(key, entities);
            return entities;
        }
    }
}
