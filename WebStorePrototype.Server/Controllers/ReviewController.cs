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
    public class ReviewController : ControllerBase
    {
        private readonly BaseRepo<DbContext, Review> _reviewRepo;
        private readonly RedisService<Review> _redisService;

        public ReviewController(DbContext dbContext, RedisService<Review> redisService)
        {
            _reviewRepo = new BaseRepo<DbContext, Review>(dbContext);
            _redisService = redisService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Review?>> Get(Guid id)
        {
            RedisKey redisKey = new RedisKey($"review:{id}");
            
            Review? review = await _redisService.GetAsync(redisKey);
            if(review != null) { return Ok(review); }

            review = await _reviewRepo.GetAsync(id);
            if (review == null) { return NoContent(); }

            await _redisService.SetAsync(redisKey, review);
            return Ok(review);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAll()
        {
            RedisKey redisKey = new RedisKey("reviews:all");

            IEnumerable<Review> review = await _redisService.GetListAsync(redisKey);
            if(review != null) { return Ok(review); }

            review = await _reviewRepo.GetAllAsync();
            if (review == null)
            {
                return NoContent();
            }

            var result = await _reviewRepo.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Review>> Create(Review review)
        {
            await _reviewRepo.AddAsync(review);
            await _reviewRepo.SaveAsync();

            await _redisService.SetAsync($"review:{review.Id}", review);
            await _redisService.DeleteAsync("reviews:all");
            return Ok(review);

        }

        [HttpPut]
        public async Task<ActionResult<Review>> Update(Review review)
        {
            _reviewRepo.Update(review);
            await _reviewRepo.SaveAsync();
            
            await _redisService.SetAsync($"review:{review.Id}", review);
            await _redisService.DeleteAsync("reviews:all");
            return Ok(review);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var review = await _reviewRepo.GetAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            _reviewRepo.Delete(review);
            await _reviewRepo.SaveAsync();

            await _redisService.DeleteAsync($"review:{id}");
            await _redisService.DeleteAsync("reviews:all");
            return NoContent();
        }
    }
}
