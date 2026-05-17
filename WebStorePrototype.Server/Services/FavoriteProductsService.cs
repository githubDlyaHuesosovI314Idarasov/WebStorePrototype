using DAL.Models;
using DAL.Repos;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;
using WebStorePrototype.Server.Services.Base;


namespace WebStorePrototype.Server.Services
{
    public class FavoriteProductsService : IFavoriteProductsService
    {
        private readonly String CookieKey = "favorite_products";
        private readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(30);

        private readonly BaseRepo<DbContext, FavoriteProduct> _favoritesRepo;
        private readonly BaseRepo<DbContext, Product> _productsRepo;
        private readonly RedisService<FavoriteProduct> _redisService;
        private readonly KeycloakUserService _keycloakUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CookieOptions _cookieOptions;

        public FavoriteProductsService(DbContext context, RedisService<FavoriteProduct> redisService, 
            KeycloakUserService keycloakUserService, IHttpContextAccessor httpContextAccessor, CookieOptions cookieOptions) {
            
            _favoritesRepo = new BaseRepo<DbContext, FavoriteProduct>(context);
            _productsRepo = new BaseRepo<DbContext, Product>(context);
            _redisService = redisService;
            _keycloakUserService = keycloakUserService;
            _httpContextAccessor = httpContextAccessor;
            _cookieOptions = cookieOptions;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(String? userId)
        {
            var user = userId != null ? await _keycloakUserService.GetUserAsync(userId) : null;
            return user != null ? await GetAuthenticatedAsync(userId!) : await GetAnonymousAsync();
        }
        
        public async Task AddProductAsync(Guid productId, String? userId)
        {
            var ids = ReadCookieIds();
            if (!ids.Contains(productId))
            {
                ids.Add(productId);
                WriteCookieIds(ids);
            }

            var user = userId != null ? await _keycloakUserService.GetUserAsync(userId) : null;
            if (user == null) return;

            await PersistToDbAsync(userId!, productId);
            await _redisService.DeleteAsync(CacheKey(userId!));
        }

        public async Task RemoveProductAsync(Guid productId, String? userId)
        {
            var ids = ReadCookieIds();
            if (ids.Remove(productId))
                WriteCookieIds(ids);

            var user = userId != null ? await _keycloakUserService.GetUserAsync(userId) : null;
            if (user == null) return;

            await RemoveFromDbAsync(userId!, productId);
            await _redisService.DeleteAsync(CacheKey(userId!));

        }

        public async Task MergeCookieIntoUserAsync(String userId)
        {
            var ids = ReadCookieIds();
            if (!ids.Any()) return;

            foreach (var id in ids)
                await PersistToDbAsync(userId, id);

            Http.Response.Cookies.Delete(CookieKey);
            await _redisService.DeleteAsync(CacheKey(userId));
        }

        private async Task<IEnumerable<Product>> GetAnonymousAsync()
        {
            var ids = ReadCookieIds();
            if (!ids.Any()) return Enumerable.Empty<Product>();

            return (await _productsRepo.GetAllAsync())
                .Where(p => ids.Contains(p.Id))
                .ToList();
        }

        private async Task<IEnumerable<Product>> GetAuthenticatedAsync(String userId)
        {
            var key = CacheKey(userId);

            if (await _redisService.IsRedisAvailable(key))
            {
                var cached = await _redisService.GetListAsync(key);
                if (cached.Any())
                    return cached.SelectMany(fp => fp.Products ?? Enumerable.Empty<Product>());
            }

            var record = await GetOrCreateRecordAsync(userId);
            await _redisService.SetListAsync(key, new[] { record }, CacheTtl);
            return record.Products ?? Enumerable.Empty<Product>();
        }

        private async Task PersistToDbAsync(String userId, Guid productId)
        {
            var record = await GetOrCreateRecordAsync(userId);
            var product = await _productsRepo.GetAsync(productId);
            if (product == null) return;

            var list = record.Products?.ToList() ?? new List<Product>();
            if (list.Any(p => p.Id == productId)) return;

            list.Add(product);
            record.Products = list;
            _favoritesRepo.Update(record);
            await _favoritesRepo.SaveAsync();
        }

        private async Task RemoveFromDbAsync(String userId, Guid productId)
        {
            var record = await GetOrCreateRecordAsync(userId);
            var list = record.Products?.ToList() ?? new List<Product>();

            if (list.RemoveAll(p => p.Id == productId) == 0) return;

            record.Products = list;
            _favoritesRepo.Update(record);
            await _favoritesRepo.SaveAsync();
        }

        private async Task<FavoriteProduct> GetOrCreateRecordAsync(String userId)
        {
            var all = await _favoritesRepo.GetAllAsync();
            var record = all.FirstOrDefault(fp => fp.UserId == userId);
            if (record != null) return record;

            record = new FavoriteProduct { UserId = userId, Products = new List<Product>() };
            await _favoritesRepo.AddAsync(record);
            await _favoritesRepo.SaveAsync();
            return record;
        }

        private List<Guid> ReadCookieIds()
        {
            var raw = Http.Request.Cookies[CookieKey];
            if (String.IsNullOrEmpty(raw)) return new List<Guid>();

            try { return JsonSerializer.Deserialize<List<Guid>>(raw) ?? new List<Guid>(); }
            catch { return new List<Guid>(); }
        }

        private void WriteCookieIds(List<Guid> ids) =>
            Http.Response.Cookies.Append(CookieKey, JsonSerializer.Serialize(ids), _cookieOptions);

        private static String CacheKey(String userId) => $"favorite_products:{userId}";
        private HttpContext Http => _httpContextAccessor.HttpContext!;
    }
}
