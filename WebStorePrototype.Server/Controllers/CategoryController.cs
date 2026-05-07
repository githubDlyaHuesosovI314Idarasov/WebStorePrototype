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
    public class CategoryController : ControllerBase
    {
        private readonly RedisKey _redisKey = "categories:all";
        private readonly BaseRepo<DbContext, Category> _categoryRepo;
        private readonly RedisService<Category> _redisService;

        public CategoryController(DbContext dbContext)
        {
            _categoryRepo = new BaseRepo<DbContext, Category>(dbContext);
            _redisService = new RedisService<Category>(_categoryRepo, _redisKey);
        }

        [HttpGet("{id}")]
        public async Task<Category?> Get(Guid id)
        {

            if (_redisService.IsRedisAvailable())
            {
                return await _redisService.GetFromRedis(id);
            }

            var category = await _categoryRepo.GetAsync(id);
            await _redisService.SetOneEntityToRedis(category);
            return category;

        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetAll()
        {
            if (_redisService.IsRedisAvailable())
            {
                return (await _redisService.GetAllFromRedis()).ToList();
            }

            var categories = await _categoryRepo.GetAllAsync();
            await _redisService.SetAllEntitiesToRedis();
            return categories;

        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _categoryRepo.AddAsync(category);
            await _categoryRepo.SaveAsync();
            return Ok(category);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Category category)
        {
            _categoryRepo.Update(category);
            await _categoryRepo.SaveAsync();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryRepo.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepo.Delete(category);
            await _categoryRepo.SaveAsync();
            return Ok();

        }
    }
}
