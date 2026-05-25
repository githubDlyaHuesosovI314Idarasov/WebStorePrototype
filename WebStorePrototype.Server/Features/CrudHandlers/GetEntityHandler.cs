using DAL;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebStorePrototype.Server.Features.Base;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Features.CrudHandlers
{
    public class GetEntityHandler<T> : IRequestHandler<GetByIdQuery<T>, T?> where T : Entity
    {
        private readonly BaseRepo<DbContext, T> _repo;
        private readonly RedisService<T> _redis;
        private readonly String _cachePrefix;

        public GetEntityHandler(DbContext context, RedisService<T> redis)
        {
            _repo = new BaseRepo<DbContext, T>(context);
            _redis = redis;
            _cachePrefix = typeof(T).Name.ToLower(); // "order", "product" и т.д.
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
