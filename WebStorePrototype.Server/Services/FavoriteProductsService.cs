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

        public async Task<IEnumerable<FavoriteProduct>> GetProductsAsync(String? userId)
        {
            var user = userId != null ? await _keycloakUserService.GetUserAsync(userId) : null;
            return user != null ? await GetAuthenticatedAsync(userId) : await GetAnonymousAsync();
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

        private async Task<IEnumerable<FavoriteProduct>> GetAnonymousAsync()
        {
            var ids = ReadCookieIds();
            if (!ids.Any()) return Enumerable.Empty<FavoriteProduct>();

            return (await _productsRepo.GetAllAsync())
                .Where(p => ids.Contains(p.Id))
                .Select(p => new FavoriteProduct { ProductId = p.Id, Product = p })
                .ToList();
        }

        private async Task<IEnumerable<FavoriteProduct>> GetAuthenticatedAsync(String userId)
        {
            var key = CacheKey(userId);

            if (await _redisService.IsRedisAvailable(key))
            {
                var cached = await _redisService.GetListAsync(key);
                if (cached.Any()) return cached;
            }

            var all = await _favoritesRepo.GetAllAsync();

            var records = all.ToList().Where(x => x.UserId == userId);
            await _redisService.SetListAsync(key, records, CacheTtl);
            return records;
        }

        private async Task PersistToDbAsync(String userId, Guid productId)
        {
            var all = await _favoritesRepo.GetAllAsync();
            var existing = all.FirstOrDefault(v => v.UserId == userId && v.ProductId == productId);

            if (existing != null)
            {
                _favoritesRepo.Update(existing);
            }
            else
            {
                var record = new FavoriteProduct
                {
                    UserId = userId,
                    ProductId = productId,
                    Product = await _productsRepo.GetAsync(productId)
                };
                await _favoritesRepo.AddAsync(record);
            }

            await _favoritesRepo.SaveAsync();
        }

        private async Task RemoveFromDbAsync(String userId, Guid productId)
        {
            
            var favoriteProdcts = await _favoritesRepo.GetAllAsync();
            var favoriteProduct = favoriteProdcts.FirstOrDefault(v => v.UserId == userId && v.ProductId == productId);
            if (favoriteProduct != null)
            {
                _favoritesRepo.Delete(favoriteProduct);
                await _redisService.DeleteAsync(CacheKey(userId));
            }

            return;
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
