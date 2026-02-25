using DAL.Models;
using DAL.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly RedisKey _redisKey = "categories";
        private readonly BaseRepo<DbContext, Location> _locationRepo;
        private readonly RedisService<Location> _redisService;

        public LocationController(DbContext dbContext)
        {
            _locationRepo = new BaseRepo<DbContext, Location>(dbContext);
            _redisService = new RedisService<Location>(_locationRepo, _redisKey);
        }

        [HttpGet("{id}")]
        public async Task<Location?> Get(Guid id)
        {
            if (_redisService.IsRedisAvailable())
            {
                return await _redisService.GetFromRedis(id);
            }

            var location = await _locationRepo.GetAsync(id);
            await _redisService.SetOneEntityToRedis(location);
            return location;

        }

        [HttpGet]
        public async Task<IEnumerable<Location>> GetAll()
        {
            if (_redisService.IsRedisAvailable())
            {
                return (await _redisService.GetAllFromRedis()).ToList();
            }

            var locations = await _locationRepo.GetAllAsync();
            await _redisService.SetAllEntitiesToRedis();
            return locations;

        }

        [HttpPost]
        public async Task<IActionResult> Create(Location location)
        {
            await _locationRepo.AddAsync(location);
            await _locationRepo.SaveAsync();
            return Ok(location);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Location location)
        {
            _locationRepo.Update(location);
            await _locationRepo.SaveAsync();
            return Ok(location);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var location = await _locationRepo.GetAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            _locationRepo.Delete(location);
            await _locationRepo.SaveAsync();
            return Ok();

        }

    }
}
