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
    public class GetEntityHandler<T> : IRequestHandler<GetByIdQuery<T>, T?> where T : Entity
    {
        private readonly Repo<T> _repo;
        private readonly RedisService<T> _redis;
        private readonly String _cachePrefix;

        public GetEntityHandler(WebStoreDBContext context, RedisService<T> redis, HybridCache cache)
        {
            _repo = new Repo<T>(context, cache);
            _redis = redis;
            _cachePrefix = typeof(T).Name.ToLower();
        }

        public async Task<T?> Handle(GetByIdQuery<T> request, CancellationToken cancellationToken)
        {
            RedisKey key = $"{_cachePrefix}:{request.Id}";

            var cached = await _redis.GetAsync(key);
            if (cached != null) return cached;

            var entity = await _repo.GetAsync(request.Id);
            if (entity == null) return null;

            await _redis.SetAsync(key, entity);
            return entity;
        }
    }
}
