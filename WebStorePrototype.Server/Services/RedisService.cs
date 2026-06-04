using DAL;
using DAL.Models;
using DAL.Repos;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Registry;
using StackExchange.Redis;
using System.Diagnostics;
using System.Text.Json;

namespace WebStorePrototype.Server.Services
{
    public class RedisService<T> where T : Entity
    {
        private readonly IDatabase _redis;
        private readonly ResiliencePipeline _pipeline;
        public RedisService(IConnectionMultiplexer multiplexer, ResiliencePipelineProvider<String> pipelineProvider) 
        {
            _redis = multiplexer.GetDatabase();

            _pipeline = pipelineProvider.GetPipeline("redis-pipeline");
        }
        
        public async Task<T?> GetAsync(RedisKey redisKey)
        {
            return await _pipeline.ExecuteAsync(async ct =>
            {
                var value = await _redis.StringGetAsync(redisKey);
                return value.HasValue ? JsonSerializer.Deserialize<T>(value.ToString()) : null;
            });
        }
        
        public async Task<IEnumerable<T>> GetListAsync(RedisKey redisKey)
        {
            return await _pipeline.ExecuteAsync(async ct =>
            {
                var entities = await _redis.ListRangeAsync(redisKey);
                if (entities.Length == 0) return new List<T>();
                return entities.Select(x => JsonSerializer.Deserialize<T>(x.ToString())).Where(x => x != null).ToList()!;
            });
        } 

        public async Task SetAsync(RedisKey redisKey, T entity, TimeSpan? ttl = null)
        {
            await _pipeline.ExecuteAsync(async ct =>
            {
                await _redis.StringSetAsync(redisKey, JsonSerializer.Serialize(entity), ttl ?? TimeSpan.FromMinutes(5));
            });
        }

        public async Task SetAsync(RedisKey redisKey, T entity)
        {
            await _pipeline.ExecuteAsync(async ct =>
            {
                await _redis.StringSetAsync(redisKey, JsonSerializer.Serialize(entity), TimeSpan.FromMinutes(5));
            });
        }

        public async Task SetListAsync(RedisKey redisKey, IEnumerable<T> entities, TimeSpan? ttl = null)
        {
            await _pipeline.ExecuteAsync(async ct =>
            {
                var tran = _redis.CreateTransaction();
                _ = tran.KeyDeleteAsync(redisKey);

                foreach (var entity in entities)
                {
                    _ = tran.ListRightPushAsync(redisKey, JsonSerializer.Serialize(entity));
                }

                _ = tran.KeyExpireAsync(redisKey, ttl ?? TimeSpan.FromMinutes(5));
                await tran.ExecuteAsync();
            });
        }

        public async Task DeleteAsync(RedisKey redisKey)
        {
            await _pipeline.ExecuteAsync(async ct =>
            {
                await _redis.KeyDeleteAsync(redisKey);
            });
        }

        public async Task<Boolean> IsRedisAvailable(RedisKey redisKey)
        {
            try
            {
                return await _pipeline.ExecuteAsync(async ct =>  await _redis.KeyExistsAsync(redisKey));
            }
            catch (RedisException) { return false; }
        }

        public IBatch CreateBatch()
        {
            return _redis.CreateBatch();
        }
    }
}
