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

        private readonly BaseRepo<DbContext, Order> _orderRepo;
        private readonly RedisService<Order> _redisService;

        public OrderController(DbContext dbContext, RedisService<Order> redisService)
        {
            _orderRepo = new BaseRepo<DbContext, Order>(dbContext);
            _redisService = redisService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Order?>> Get(Guid id)
        {
            RedisKey redisKey = new RedisKey($"order:{id}");

            Order? order = await _redisService.GetAsync(redisKey);
            if (order != null) return Ok(order);

            order = await _orderRepo.GetAsync(id);
            if (order == null) return NotFound();

            await _redisService.SetAsync(redisKey, order);
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            RedisKey redisKey = new RedisKey($"orders:all");
            
            if (await _redisService.IsRedisAvailable(redisKey))
            {
                IEnumerable<Order> cached = await _redisService.GetListAsync(redisKey);
                return Ok(cached);
            }

            IEnumerable<Order> orders = await _redisService.GetListAsync(redisKey);
            await _redisService.SetListAsync(redisKey, orders);
            return Ok(orders);

        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveAsync();

            await _redisService.SetAsync($"order:{order.Id}", order);
            await _redisService.DeleteAsync("orders:all");
            return Ok(order);

        }

        [HttpPut]
        public async Task<IActionResult> Update(Order order)
        {
            _orderRepo.Update(order);
            await _orderRepo.SaveAsync();
            
            await _redisService.SetAsync($"order:{order.Id}", order);
            await _redisService.DeleteAsync("orders:all");
            return Ok(order);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _orderRepo.GetAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            _orderRepo.Delete(order);
            await _orderRepo.SaveAsync();

            await _redisService.DeleteAsync($"order:{id}");
            await _redisService.DeleteAsync("orders:all");

            return Ok();

        }
    }
}
