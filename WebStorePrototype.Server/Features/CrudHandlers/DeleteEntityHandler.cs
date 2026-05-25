using DAL;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebStorePrototype.Server.Features.Base;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Features.CrudHandlers
{
    public class DeleteEntityHandler<T> : IRequestHandler<DeleteCommand<T>> where T : Entity
    {
        private readonly BaseRepo<DbContext, T> _repo;
        private readonly RedisService<T> _redis;
        private readonly string _cachePrefix;

        public DeleteEntityHandler(DbContext context, RedisService<T> redis)
        {
            _repo = new BaseRepo<DbContext, T>(context);
            _redis = redis;
            _cachePrefix = typeof(T).Name.ToLower();
        }

        public async Task Handle(DeleteCommand<T> request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetAsync(request.Id);
            if (entity == null) return;

            _repo.Delete(entity);
            await _repo.SaveAsync();

            await _redis.DeleteAsync($"{_cachePrefix}:{request.Id}");
            await _redis.DeleteAsync($"{_cachePrefix}s:all");

            return;
        }
    }
}
