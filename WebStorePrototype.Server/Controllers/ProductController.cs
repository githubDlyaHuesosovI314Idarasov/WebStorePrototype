using DAL.EF;
using DAL.Models;
using DAL.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Data;
using System.Data.Common;
using System.Text.Json;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly BaseRepo<DbContext, Product> _productRepo;
        private readonly RedisService<Product> _redisService;
        public ProductController(DbContext dbContext, RedisService<Product> redisService)
        {
            _productRepo = new BaseRepo<DbContext, Product>(dbContext);
            _redisService = redisService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Product?>> Get(Guid id)
        {
            RedisKey redisKey = $"product:{id}";

            Product? product = await _redisService.GetAsync(redisKey);
            if(product != null) { return Ok(product); }
        
            product = await _productRepo.GetAsync(id);
            if (product == null) { return NotFound(); }

            await _redisService.SetAsync(redisKey, product);
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            RedisKey redisKey = "products:all";

            if(await _redisService.IsRedisAvailable(redisKey))
            {
                IEnumerable<Product> cached = await _redisService.GetListAsync(redisKey);
                if(cached.Count() > 0) {
                    return Ok(cached);
                }
       
            }

            IEnumerable<Product> products = await _productRepo.GetAllAsync();
            await _redisService.SetListAsync(redisKey, products);
            return Ok(products);

        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            await _productRepo.AddAsync(product);
            await _productRepo.SaveAsync();

            await _redisService.SetAsync($"product:{product.Id}", product);
            await _redisService.DeleteAsync("products:all");
            return CreatedAtAction(nameof(Get), new {id = product.Id}, product);
        }

        [HttpPut]
        public async Task<ActionResult<Product>> Update(Product product)
        {
            _productRepo.Update(product);
            await _productRepo.SaveAsync();

            await _redisService.SetAsync($"product:{product.Id}", product);
            await _redisService.DeleteAsync("products:all");
            return Ok(product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productRepo.GetAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _productRepo.Delete(product);
            await _productRepo.SaveAsync();
            
            await _redisService.DeleteAsync($"product:{id}");
            await _redisService.DeleteAsync("products:all");
            return NoContent();

        }

        [HttpGet("batch")]
        public async Task<ActionResult<IEnumerable<Product>>> GetBatch([FromQuery] List<Guid> ids)
        {
            if (!ids.Any()) return Ok(Enumerable.Empty<Product>());

            var result = new List<Product>();
            var missing = new List<Guid>();

            foreach (var id in ids)
            {
                var cached = await _redisService.GetAsync($"product:{id}");
                if (cached != null)
                    result.Add(cached);
                else
                    missing.Add(id);
            }

            if (missing.Any())
            {
                var fromDb = (await _productRepo.GetAllAsync())
                    .Where(p => missing.Contains(p.Id))
                    .ToList();

                foreach (var p in fromDb)
                    await _redisService.SetAsync($"product:{p.Id}", p);

                result.AddRange(fromDb);
            }

            return Ok(result);
        }

    }
}
