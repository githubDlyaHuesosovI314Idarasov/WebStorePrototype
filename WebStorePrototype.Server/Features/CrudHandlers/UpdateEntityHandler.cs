using DAL;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebStorePrototype.Server.Features.Base;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Features.CrudHandlers
{
    public class UpdateEntityHandler<T> : IRequestHandler<UpdateCommand<T>, T> where T : Entity
    {
        private readonly BaseRepo<DbContext, T> _repo;
        private readonly RedisService<T> _redis;
        private readonly string _cachePrefix;

        public UpdateEntityHandler(DbContext context, RedisService<T> redis)
        {
            _repo = new BaseRepo<DbContext, T>(context);
            _redis = redis;
            _cachePrefix = typeof(T).Name.ToLower();
        }

        public async Task<T> Handle(UpdateCommand<T> request, CancellationToken cancellationToken)
        {
            _repo.Update(request.Entity);
            await _repo.SaveAsync();

            await _redis.SetAsync($"{_cachePrefix}:{request.Entity.Id}", request.Entity);
            await _redis.DeleteAsync($"{_cachePrefix}s:all");

            return request.Entity;
        }
    }
}
