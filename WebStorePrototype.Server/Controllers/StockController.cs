using DAL.Models;
using DAL.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly BaseRepo<DbContext, Stock> _stockRepo;
        private readonly RedisService<Stock> _redisService;

        public StockController(DbContext dbContext, RedisService<Stock> redisService)
        {
            _stockRepo = new BaseRepo<DbContext, Stock>(dbContext);
            _redisService = redisService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Stock?>> Get(Guid id)
        {
            RedisKey redisKey = $"stocks:{id}";

            Stock? stock = await _redisService.GetAsync(redisKey);
            if (stock != null) { return stock; }

            stock = await _stockRepo.GetAsync(id);
            if(stock == null) { return NoContent(); }
            
            await _redisService.SetAsync(redisKey, stock);
            return Ok(stock);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetAll()
        {
            RedisKey redisKey = new RedisKey("stocks:all");
            
            if(await _redisService.IsRedisAvailable(redisKey))
            {
                IEnumerable<Stock> cached = await _redisService.GetListAsync(redisKey);
                if (cached.Count() > 0) {
                    return Ok(cached);
                }
            }

            IEnumerable<Stock> stocks = await _stockRepo.GetAllAsync();
            await _redisService.SetListAsync(redisKey, stocks);
            return Ok(stocks);

        }

        [HttpPost]
        public async Task<ActionResult<Stock>> Create(Stock stock)
        {
            await _stockRepo.AddAsync(stock);
            await _stockRepo.SaveAsync();

            await _redisService.SetAsync($"stocks:{stock.Id}", stock);
            await _redisService.DeleteAsync("stocks:all");
            return Ok(stock);
        }

        [HttpPut]
        public async Task<ActionResult<Stock>> Update(Stock stock)
        {
            _stockRepo.Update(stock);
            await _stockRepo.SaveAsync();

            await _redisService.SetAsync($"stocks:{stock.Id}", stock);
            await _redisService.DeleteAsync("stocks:all");
            return Ok(stock);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var stock = await _stockRepo.GetAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            _stockRepo.Delete(stock);
            await _stockRepo.SaveAsync();

            await _redisService.DeleteAsync($"stocks:{id}");
            await _redisService.DeleteAsync("stocks:all");
            return Ok();

        }
    }
}
