using DAL.Models;
using DAL.Repos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BaseRepo<DbContext, Category> _categoryRepo;
        private readonly RedisService<Category> _redisService;
        public CategoryService(DbContext context, RedisService<Category> redisService) {

            _categoryRepo = new BaseRepo<DbContext, Category>(context);
            _redisService = redisService;
        }

        public async Task<ActionResult<Category?>> GetAsync(Guid id)
        {
            RedisKey redisKey = new RedisKey($"category:{id}");

            Category? category = await _redisService.GetAsync(redisKey);
            if (category != null) { return new OkObjectResult(category); }

            category = await _categoryRepo.GetAsync(id);
            if (category == null) { return new NotFoundResult(); }

            await _redisService.SetAsync(redisKey, category);
            return new OkObjectResult(category);
        }
        public async Task<ActionResult<IEnumerable<Category>>> GetAllAsync()
        {
            RedisKey redisKey = new RedisKey("categories:all");
            if (await _redisService.IsRedisAvailable(redisKey))
            {
                IEnumerable<Category> cached = await _redisService.GetListAsync(redisKey);
                if (cached.Count() > 0)
                {
                    return new OkObjectResult(cached);
                }
            }

            var categories = await _categoryRepo.GetAllAsync();
            await _redisService.SetListAsync(redisKey, categories);
            return new OkObjectResult(categories);
        }

        public async Task<IActionResult> Create(Category category)
        {
            await _categoryRepo.AddAsync(category);
            await _categoryRepo.SaveAsync();

            await _redisService.SetAsync($"category:{category.Id}", category);
            await _redisService.DeleteAsync("categories:all");
            return new OkObjectResult(category);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryRepo.GetAsync(id);
            if (category == null)
            {
                return new NotFoundResult();
            }
            _categoryRepo.Delete(category);
            await _categoryRepo.SaveAsync();

            await _redisService.DeleteAsync($"category:{id}");
            await _redisService.DeleteAsync("categories:all");
            return new OkResult();
        }

        public async Task<IActionResult> Update(Category category)
        {
            _categoryRepo.Update(category);
            await _categoryRepo.SaveAsync();

            await _redisService.SetAsync($"category:{category.Id}", category);
            await _redisService.DeleteAsync("categories:all");
            return new OkObjectResult(category);
        }
    }
}
