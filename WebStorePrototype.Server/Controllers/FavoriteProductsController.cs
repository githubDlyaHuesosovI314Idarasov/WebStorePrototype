using DAL.Models;
using DAL.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Net.WebSockets;
using System.Text.Json;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteProductsController : Controller
    {
        private readonly RedisService<FavoriteProducts> _redisService;
        private readonly BaseRepo<DbContext, FavoriteProducts> _favoriteProductsRepo;
        private readonly BaseRepo<DbContext, Product> _productsRepo;
        private readonly KeycloakUserService _keycloakUserService;
        private readonly CookieOptions _cookieOptions;
        private readonly String _cookieKey = "favorite_products";

        public FavoriteProductsController(DbContext context, KeycloakUserService keycloakUserService, CookieOptions cookieOptions, RedisService<FavoriteProducts> redisService)
        {
            _favoriteProductsRepo = new BaseRepo<DbContext, FavoriteProducts>(context);
            _productsRepo = new BaseRepo<DbContext, Product>(context);
            _redisService = redisService;
            _keycloakUserService = keycloakUserService;
            _cookieOptions = cookieOptions;
        }

        [HttpGet("{userId?}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] String? userId)
        {
            var user = userId != null
                ? await _keycloakUserService.GetUserAsync(userId)
                : null;

            return Ok(user != null
                ? await GetAuthenticatedProductsAsync(userId!)
                : await GetAnonymousProductsAsync());
        }


        [HttpPost("{productId:guid}")]
        public async Task<IActionResult> AddProduct(Guid productId, [FromQuery] String? userId)
        {
            var cookieIds = ReadCookieIds();
            if (!cookieIds.Contains(productId))
            {
                cookieIds.Add(productId);
                WriteCookieIds(cookieIds);
            }

            var user = userId != null
                ? await _keycloakUserService.GetUserAsync(userId)
                : null;

            if (user != null)
            {
                await PersistToDbAsync(userId!, productId);
                await _redisService.DeleteAsync(CacheKey(userId!));
            }

            return NoContent();
        }

        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> RemoveProduct(Guid productId, [FromQuery] String? userId)
        {
            var cookieIds = ReadCookieIds();
            if (cookieIds.Remove(productId))
                WriteCookieIds(cookieIds);

            var user = userId != null
                ? await _keycloakUserService.GetUserAsync(userId)
                : null;

            if (user != null)
            {
                await RemoveFromDbAsync(userId!, productId);
                await _redisService.DeleteAsync(CacheKey(userId!));
            }

            return NoContent();
        }


        [HttpPost("merge")]
        public async Task<IActionResult> MergeCookieIntoUser([FromQuery] String userId)
        {
            if (String.IsNullOrWhiteSpace(userId))
                return BadRequest("userId is required.");

            var cookieIds = ReadCookieIds();
            if (!cookieIds.Any()) return NoContent();

            foreach (var id in cookieIds)
                await PersistToDbAsync(userId, id);

            Response.Cookies.Delete(_cookieKey);
            await _redisService.DeleteAsync(CacheKey(userId));

            return NoContent();
        }


        private async Task<IEnumerable<Product>> GetAnonymousProductsAsync()
        {
            var ids = ReadCookieIds();
            if (!ids.Any()) return Enumerable.Empty<Product>();

            return (await _productsRepo.GetAllAsync())
                .Where(p => ids.Contains(p.Id))
                .ToList();
        }

        private async Task<IEnumerable<Product>> GetAuthenticatedProductsAsync(String userId)
        {
            var key = CacheKey(userId);
            
            if (await _redisService.IsRedisAvailable(key))
            {
                var cached = await _redisService.GetListAsync(key);
                if (cached.Any())
                    return cached.SelectMany(fp => fp.Products ?? Enumerable.Empty<Product>());
            }

            var record = await GetOrCreateRecordAsync(userId);
            await _redisService.SetListAsync(key, new[] { record }, TimeSpan.FromMinutes(30));

            return record.Products ?? Enumerable.Empty<Product>();
        }

        private async Task PersistToDbAsync(String userId, Guid productId)
        {
            var record = await GetOrCreateRecordAsync(userId);
            var product = await _productsRepo.GetAsync(productId);
            if (product == null) return;

            var list = record.Products?.ToList() ?? new List<Product>();
            if (!list.Any(p => p.Id == productId))
            {
                list.Add(product);
                record.Products = list;
                _favoriteProductsRepo.Update(record);
                await _favoriteProductsRepo.SaveAsync();
            }
        }

        private async Task RemoveFromDbAsync(String userId, Guid productId)
        {
            var record = await GetOrCreateRecordAsync(userId);
            var list = record.Products?.ToList() ?? new List<Product>();

            if (list.RemoveAll(p => p.Id == productId) > 0)
            {
                record.Products = list;
                _favoriteProductsRepo.Update(record);
                await _favoriteProductsRepo.SaveAsync();
            }
        }

        private async Task<FavoriteProducts> GetOrCreateRecordAsync(String userId)
        {
            var all = await _favoriteProductsRepo.GetAllAsync();
            var record = all.FirstOrDefault(fp => fp.UserId == userId);

            if (record != null) return record;

            record = new FavoriteProducts { UserId = userId, Products = new List<Product>() };
            await _favoriteProductsRepo.AddAsync(record);
            await _favoriteProductsRepo.SaveAsync();
            return record;
        }


        private List<Guid> ReadCookieIds()
        {
            var raw = Request.Cookies[_cookieKey];
            if (String.IsNullOrEmpty(raw)) return new List<Guid>();

            try { return JsonSerializer.Deserialize<List<Guid>>(raw) ?? new List<Guid>(); }
            catch (JsonException) { return new List<Guid>(); }
        }

        private void WriteCookieIds(List<Guid> ids)
        {
            Response.Cookies.Append(_cookieKey, JsonSerializer.Serialize(ids), _cookieOptions);
        }

        private static String CacheKey(String userId) => $"favorite_products:{userId}";
    }

}

