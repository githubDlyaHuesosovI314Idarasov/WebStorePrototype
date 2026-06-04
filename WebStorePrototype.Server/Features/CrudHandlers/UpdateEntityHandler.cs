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
    public class UpdateEntityHandler<T> : IRequestHandler<UpdateCommand<T>, T> where T : Entity
    {
        private readonly Repo<T> _repo;
        private readonly RedisService<T> _redis;
        private readonly string _cachePrefix;

        public UpdateEntityHandler(WebStoreDBContext context, RedisService<T> redis, HybridCache cache)
        {
            _repo = new Repo<T>(context, cache);
            _redis = redis;
            _cachePrefix = typeof(T).Name.ToLower();
        }

        public async Task<T> Handle(UpdateCommand<T> request, CancellationToken cancellationToken)
        {
            await _repo.UpdateAsync(request.Entity);
            await _repo.SaveAsync();

            await _redis.SetAsync($"{_cachePrefix}:{request.Entity.Id}", request.Entity);
            await _redis.DeleteAsync($"{_cachePrefix}s:all");

            return request.Entity;
        }
    }
}
