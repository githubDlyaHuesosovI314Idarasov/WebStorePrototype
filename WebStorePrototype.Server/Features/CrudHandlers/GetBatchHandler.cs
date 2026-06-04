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
    public class GetBatchHandler<T> : IRequestHandler<GetBatchQuery<T>, IEnumerable<T>> where T : Entity
    {
        private readonly Repo<T> _repo;
        private readonly RedisService<T> _redis;
        private readonly String _cachePrefix;

        public GetBatchHandler(WebStoreDBContext context, RedisService<T> redis, HybridCache cache)
        {
            _repo = new Repo<T>(context, cache);
            _redis = redis;
            _cachePrefix = typeof(T).Name.ToLower();
        }

        public async Task<IEnumerable<T>> Handle(GetBatchQuery<T> request, CancellationToken cancellationToken)
        {
            if (!request.Ids.Any()) return Enumerable.Empty<T>();

            var result = new List<T>();
            var missing = new List<Guid>();

            IBatch batch = _redis.CreateBatch();
            foreach (var id in request.Ids)
            {
                var cached = await _redis.GetAsync($"{_cachePrefix}:{id}");
                if (cached != null)
                    result.Add(cached);
                else
                    missing.Add(id);
            }
            batch.Execute();

            if (missing.Any())
            {
                var fromDb = (await _repo.GetAllAsync())
                    .Where(e => missing.Contains(e.Id))
                    .ToList();

                foreach (var entity in fromDb)
                    await _redis.SetAsync($"{_cachePrefix}:{entity.Id}", entity);

                result.AddRange(fromDb);
            }

            return result.OrderBy(e => request.Ids.IndexOf(e.Id));
        }
    }
}
