using DAL;
using DAL.Models;
using DAL.Repos;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Diagnostics;
using System.Text.Json;

namespace WebStorePrototype.Server.Services
{
    public class RedisService<T> where T : Entity
    {
        private readonly IDatabase _redis;
        public RedisService(IConnectionMultiplexer multiplexer) 
        {
            _redis = multiplexer.GetDatabase();
        }
        
        public async Task<T?> GetAsync(RedisKey redisKey)
        {
            try
            {
                var value = await _redis.StringGetAsync(redisKey);
                return value.HasValue ? JsonSerializer.Deserialize<T>(value.ToString()) : null;
            }
            catch (RedisException) { return null; }
        }
        
        public async Task<IEnumerable<T>> GetListAsync(RedisKey redisKey)
        {
            try
            {
                var entities = await _redis.ListRangeAsync(redisKey);
                if (entities.Length == 0) return new List<T>();
                return entities.Select(x => JsonSerializer.Deserialize<T>(x.ToString())).Where(x => x != null).ToList()!;
            
            }
            catch(RedisException) { 
                return new List<T>(); 
            }
        }

        public async Task SetAsync(RedisKey redisKey, T entity, TimeSpan? ttl = null)
        {
            try
            {
                await _redis.StringSetAsync(redisKey, JsonSerializer.Serialize(entity), ttl ?? TimeSpan.FromMinutes(5));
            }
            catch (RedisException) { }
        }

        public async Task SetListAsync(RedisKey redisKey, IEnumerable<T> entities, TimeSpan? ttl = null)
        {
            try
            {
                var tran = _redis.CreateTransaction();
                _ = tran.KeyDeleteAsync(redisKey);

                foreach (var entity in entities)
                {
                    _ = tran.ListRightPushAsync(redisKey, JsonSerializer.Serialize(entity));
                }

                _ = tran.KeyExpireAsync(redisKey, ttl ?? TimeSpan.FromMinutes(5));
                await tran.ExecuteAsync();
            }
            catch (RedisException) { }
        }

        public async Task DeleteAsync(RedisKey redisKey)
        {
            try
            {
                await _redis.KeyDeleteAsync(redisKey);
            }
            catch (RedisException) { }
        }

        public async Task<Boolean> IsRedisAvailable(RedisKey redisKey)
        {
            try
            {
                return await _redis.KeyExistsAsync(redisKey);
            }
            catch (RedisException) { return false; }
        }

    }
}
