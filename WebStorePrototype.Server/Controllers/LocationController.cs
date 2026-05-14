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
        private readonly BaseRepo<DbContext, Location> _locationRepo;
        private readonly RedisService<Location> _redisService;

        public LocationController(DbContext dbContext, RedisService<Location> redisService)
        {
            _locationRepo = new BaseRepo<DbContext, Location>(dbContext);
            _redisService = redisService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Location?>> Get(Guid id)
        {
            RedisKey redisKey = new RedisKey($"locations:{id}");

            Location? location = await _redisService.GetAsync(redisKey);
            if (location != null) { return Ok(location); }

            location = await _locationRepo.GetAsync(id);
            if (location == null) { return NotFound(); }

            await _redisService.SetAsync(redisKey, location);
            return Ok(location);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetAll()
        {
            RedisKey redisKey = new RedisKey("locations:all");
            
            if(await _redisService.IsRedisAvailable(redisKey)) {
                IEnumerable<Location> cached = await _redisService.GetListAsync(redisKey);
                if (cached.Count() > 0)
                {
                    return Ok(cached);
                }
            }

            IEnumerable<Location> locations = await _locationRepo.GetAllAsync();
            await _redisService.SetListAsync(redisKey, locations);
            return Ok(locations);

        }

        [HttpPost]
        public async Task<ActionResult<Location>> Create(Location location)
        {
            await _locationRepo.AddAsync(location);
            await _locationRepo.SaveAsync();
            
            await _redisService.SetAsync($"location:{location.Id}", location);
            await _redisService.DeleteAsync("locations:all");
            return Ok(location);
        }

        [HttpPut]
        public async Task<ActionResult<Location>> Update(Location location)
        {
            _locationRepo.Update(location);
            await _locationRepo.SaveAsync();

            await _redisService.SetAsync($"location:{location.Id}", location);
            await _redisService.DeleteAsync("locations:all");
            return Ok(location);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var location = await _locationRepo.GetAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            _locationRepo.Delete(location);
            await _locationRepo.SaveAsync();
            
            await _redisService.DeleteAsync($"location:{id}");
            await _redisService.DeleteAsync("locations:all");
            return NoContent();

        }

    }
}
