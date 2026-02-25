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
        private readonly RedisKey _redisKey = "productImages";
        private readonly BaseRepo<DbContext, ProductImage> _productImageRepo;
        private readonly RedisService<ProductImage> _redisService;

        public ProductImageController(DbContext dbContext)
        {
            _productImageRepo = new BaseRepo<DbContext, ProductImage>(dbContext);
            _redisService = new RedisService<ProductImage>(_productImageRepo, _redisKey);
        }

        [HttpGet("{id}")]
        public async Task<ProductImage?> Get(Guid id)
        {
            if (_redisService.IsRedisAvailable())
            {
                return await _redisService.GetFromRedis(id);
            }

            var productImage = await _productImageRepo.GetAsync(id);
            await _redisService.SetOneEntityToRedis(productImage);
            return productImage;

        }

        [HttpGet]
        public async Task<IEnumerable<ProductImage>> GetAll()
        {
            if (_redisService.IsRedisAvailable())
            {
                return (await _redisService.GetAllFromRedis()).ToList();
            }

            var productImages = await _productImageRepo.GetAllAsync();
            await _redisService.SetAllEntitiesToRedis();
            return productImages;

        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductImage productImage)
        {
            await _productImageRepo.AddAsync(productImage);
            await _productImageRepo.SaveAsync();
            return Ok(productImage);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductImage productImage)
        {
            _productImageRepo.Update(productImage);
            await _productImageRepo.SaveAsync();
            return Ok(productImage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var productImage = await _productImageRepo.GetAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }
            _productImageRepo.Delete(productImage);
            await _productImageRepo.SaveAsync();
            return Ok();

        }

    }
}
