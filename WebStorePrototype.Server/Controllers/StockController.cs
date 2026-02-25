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
    public class StockController : ControllerBase
    {
        private readonly RedisKey _redisKey = "stocks";
        private readonly BaseRepo<DbContext, Stock> _stockRepo;
        private readonly RedisService<Stock> _redisService;

        public StockController(DbContext dbContext)
        {
            _stockRepo = new BaseRepo<DbContext, Stock>(dbContext);
            _redisService = new RedisService<Stock>(_stockRepo, _redisKey);
        }

        [HttpGet("{id}")]
        public async Task<Stock?> Get(Guid id)
        {
            if (_redisService.IsRedisAvailable())
            {
                return await _redisService.GetFromRedis(id);
            }

            var stock = await _stockRepo.GetAsync(id);
            await _redisService.SetOneEntityToRedis(stock);
            return stock;

        }

        [HttpGet]
        public async Task<IEnumerable<Stock>> GetAll()
        {
            if (_redisService.IsRedisAvailable())
            {
                return (await _redisService.GetAllFromRedis()).ToList();
            }

            var stocks = await _stockRepo.GetAllAsync();
            await _redisService.SetAllEntitiesToRedis();
            return stocks;

        }

        [HttpPost]
        public async Task<IActionResult> Create(Stock stock)
        {
            await _stockRepo.AddAsync(stock);
            await _stockRepo.SaveAsync();
            return Ok(stock);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Stock stock)
        {
            _stockRepo.Update(stock);
            await _stockRepo.SaveAsync();
            return Ok(stock);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var stock = await _stockRepo.GetAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            _stockRepo.Delete(stock);
            await _stockRepo.SaveAsync();
            return Ok();

        }
    }
}
