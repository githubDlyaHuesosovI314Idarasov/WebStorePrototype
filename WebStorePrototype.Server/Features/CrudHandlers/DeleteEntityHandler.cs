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
    public class DeleteEntityHandler<T> : IRequestHandler<DeleteCommand<T>> where T : Entity
    {
        private readonly Repo<T> _repo;
        private readonly RedisService<T> _redis;
        private readonly String _cachePrefix;

        public DeleteEntityHandler(WebStoreDBContext context, RedisService<T> redis, HybridCache cache)
        {
            _repo = new Repo<T>(context, cache);
            _redis = redis;
            _cachePrefix = typeof(T).Name.ToLower();
        }

        public async Task Handle(DeleteCommand<T> request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetAsync(request.Id);
            if (entity == null) return;

            await _repo.DeleteAsync(entity);
            await _repo.SaveAsync();

            await _redis.DeleteAsync($"{_cachePrefix}:{request.Id}");
            await _redis.DeleteAsync($"{_cachePrefix}s:all");

            return;
        }
    }
}
