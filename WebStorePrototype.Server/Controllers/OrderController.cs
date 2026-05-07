using Microsoft.AspNetCore.Mvc;
using WebStorePrototype.Server.Services;
using Order = DAL.Models.Order;
using StackExchange.Redis;
using DAL.Repos;
using Microsoft.EntityFrameworkCore;

namespace WebStorePrototype.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly RedisKey _redisKey = "orders";
        private readonly BaseRepo<DbContext, Order> _orderRepo;
        private readonly RedisService<Order> _redisService;

        public OrderController(DbContext dbContext)
        {
            _orderRepo = new BaseRepo<DbContext, Order>(dbContext);
            _redisService = new RedisService<Order>(_orderRepo, _redisKey);
        }

        [HttpGet("{id}")]
        public async Task<Order?> Get(Guid id)
        {
            if (_redisService.IsRedisAvailable())
            {
                return await _redisService.GetFromRedis(id);
            }
            var order = await _orderRepo.GetAsync(id);
            await _redisService.SetOneEntityToRedis(order);
            return order;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> GetAll()
        {
            if (_redisService.IsRedisAvailable())
            {
                return (await _redisService.GetAllFromRedis()).ToList();
            }
            var orders = await _orderRepo.GetAllAsync();
            await _redisService.SetAllEntitiesToRedis();
            return orders;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveAsync();
            return Ok(order);

        }

        [HttpPut]
        public async Task<IActionResult> Update(Order order)
        {
            _orderRepo.Update(order);
            await _orderRepo.SaveAsync();
            await _redisService.SetOneEntityToRedis(order);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _orderRepo.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _orderRepo.Delete(category);
            await _orderRepo.SaveAsync();
            return Ok();

        }
    }
}
