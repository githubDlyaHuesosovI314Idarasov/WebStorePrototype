using DAL;
using DAL.Models;
using DAL.Repos;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;

namespace WebStorePrototype.Server.Services
{
    public class RedisService<T> where T : Entity
    {
        private readonly IDatabase _redis;
        private readonly RedisKey _redisKey;
        private readonly BaseRepo<DbContext, T> _repo;
        public RedisService(BaseRepo<DbContext, T> repo, RedisKey redisKey) {
            _redis = ConnectionMultiplexer.Connect("localhost:6379").GetDatabase();
            _redisKey = redisKey;
            _repo = repo;
        }
        
        public async Task<T?> GetFromRedis(Guid id)
        {
            var redisEntities = await _redis.ListRangeAsync(_redisKey);
            foreach (var redisEntity in redisEntities)
            {
                var enitity = JsonSerializer.Deserialize<T>(redisEntity.ToString());
                if (enitity!.Id == id && enitity != null)
                {
                    return enitity;
                }
            }
            return null;
        }
        public async Task<IEnumerable<T>> GetAllFromRedis()
        {
            var entities = new List<T>();
            var redisEntities = await _redis.ListRangeAsync(_redisKey);
            foreach (var redisEntity in redisEntities)
            {
               entities.Add(JsonSerializer.Deserialize<T>(redisEntities.ToString()!)!);
            }
            return entities;
        }

        public async Task SetOneEntityToRedis(T entity)
        {
            await _redis.ListRightPushAsync(_redisKey, JsonSerializer.Serialize(entity));
            await _redis.KeyExpireAsync(_redisKey, TimeSpan.FromMinutes(5));
        }

        public async Task SetAllEntitiesToRedis()
        {
            var entities = await _repo.GetAllAsync();
            await _redis.KeyDeleteAsync(_redisKey);
            foreach (var entity in entities)
            {
                await _redis.ListRightPushAsync(_redisKey, JsonSerializer.Serialize(entity));
            }
            await _redis.KeyExpireAsync(_redisKey, TimeSpan.FromMinutes(5));
        }

        public Boolean IsRedisAvailable()
        {
            if (_redis.ListRange(_redisKey) != null)
            {
                return true;
            }
            return false;
        }

    }
}
