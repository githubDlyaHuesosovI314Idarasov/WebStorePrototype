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
        private readonly BaseRepo<DbContext, Category> _categoryRepo;
        private readonly RedisService<Category> _redisService;

        public CategoryController(DbContext dbContext, RedisService<Category> redisService)
        {
            _categoryRepo = new BaseRepo<DbContext, Category>(dbContext);
            _redisService = redisService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Category?>> Get(Guid id)
        {
            RedisKey redisKey = new RedisKey($"category:{id}");
            
            Category? category = await _redisService.GetAsync(redisKey);
            if (category != null) { return Ok(category); }

            category = await _categoryRepo.GetAsync(id);
            if(category == null) { return NotFound(); }
    
            await _redisService.SetAsync(redisKey, category);
            return Ok(category);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            RedisKey redisKey = new RedisKey("categories:all");
            if (await _redisService.IsRedisAvailable(redisKey))
            {
                IEnumerable<Category> cached = await _redisService.GetListAsync(redisKey);
                if (cached.Count() > 0)
                {
                    return Ok(cached);
                }
            }

            var categories = await _categoryRepo.GetAllAsync();
            await _redisService.SetListAsync(redisKey, categories);
            return Ok(categories);

        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _categoryRepo.AddAsync(category);
            await _categoryRepo.SaveAsync();

            await _redisService.SetAsync($"category:{category.Id}", category);
            await _redisService.DeleteAsync("categories:all");
            return Ok(category);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Category category)
        {
            _categoryRepo.Update(category);
            await _categoryRepo.SaveAsync();

            await _redisService.SetAsync($"category:{category.Id}", category);
            await _redisService.DeleteAsync("categories:all");
            return Ok(category);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryRepo.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepo.Delete(category);
            await _categoryRepo.SaveAsync();

            await _redisService.DeleteAsync($"category:{id}");
            await _redisService.DeleteAsync("categories:all");
            return Ok();

        }
    }
}
