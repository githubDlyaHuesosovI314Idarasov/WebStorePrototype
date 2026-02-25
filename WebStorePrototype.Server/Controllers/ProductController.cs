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
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly RedisKey _redisKey = "products";
        private readonly BaseRepo<DbContext, Product> _productRepo;
        private readonly RedisService<Product> _redisService;

        public ProductController(DbContext dbContext) {

            _productRepo = new BaseRepo<DbContext, Product>(dbContext);
            _redisService = new RedisService<Product>(_productRepo, _redisKey);
           
        }

        [HttpGet("{id}")]
        public async Task<Product?> Get(Guid id) {

            if (_redisService.IsRedisAvailable())
            {
               return await _redisService.GetFromRedis(id);
            }

            var product = await _productRepo.GetAsync(id);
            await _redisService.SetOneEntityToRedis(product);  // TODO: set one entity to redis         
            return product;
   
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
            if (_redisService.IsRedisAvailable())
            {
                return (await _redisService.GetAllFromRedis()).ToList();
            }

            var products = await _productRepo.GetAllAsync();
            await _redisService.SetAllEntitiesToRedis();
            return products;

        }


       


    }
}
