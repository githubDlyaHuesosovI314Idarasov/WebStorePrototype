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
    public class ProductImageController : ControllerBase
    {
        private readonly BaseRepo<DbContext, ProductImage> _productImageRepo;
        private readonly RedisService<ProductImage> _redisService;

        public ProductImageController(DbContext dbContext, RedisService<ProductImage> redisService)
        {
            _productImageRepo = new BaseRepo<DbContext, ProductImage>(dbContext);
            _redisService = redisService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductImage?>> Get(Guid id)
        {
            RedisKey redisKey = $"productImages:{id}";

            ProductImage? productImage = await _productImageRepo.GetAsync(id);
            if(productImage != null) { return Ok(productImage); }

            productImage = await _productImageRepo.GetAsync(id);
            if(productImage == null) { return NotFound(); }

            await _redisService.SetAsync(redisKey, productImage);
            return productImage;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImage>>> GetAll()
        {
            RedisKey redisKey = "productImages:all";

            if (await _redisService.IsRedisAvailable(redisKey))
            {
                IEnumerable<ProductImage> cached = await _redisService.GetListAsync(redisKey);
                if (cached.Count() > 0)
                {
                    return Ok(cached);
                }
            }

            IEnumerable<ProductImage> productImages = await _productImageRepo.GetAllAsync();
            await _redisService.SetListAsync(redisKey, productImages);
            return Ok(productImages);
            
        }

        [HttpPost]
        public async Task<ActionResult<ProductImage>> Create(ProductImage productImage)
        {
            await _productImageRepo.AddAsync(productImage);
            await _productImageRepo.SaveAsync();

            await _redisService.SetAsync($"productImages:{productImage.Id}", productImage);
            await _redisService.DeleteAsync("productImages:all");
            return Ok(productImage);
        }

        [HttpPut]
        public async Task<ActionResult<ProductImage>> Update(ProductImage productImage)
        {
            _productImageRepo.Update(productImage);
            await _productImageRepo.SaveAsync();

            await _redisService.SetAsync($"productImages:{productImage.Id}", productImage);
            await _redisService.DeleteAsync("productImages:all");
            return Ok(productImage);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var productImage = await _productImageRepo.GetAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }
            _productImageRepo.Delete(productImage);
            await _productImageRepo.SaveAsync();

            await _redisService.DeleteAsync($"productImages:{id}");
            await _redisService.DeleteAsync("productImages:all");
            return NoContent();

        }

    }
}
