using DAL.Models;
using DAL.Repos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Services
{
    public class ViewedProductService : IViewedProductsService
    {
        private const String CookieKey = "viewed_products";
        private readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(30);
        public const Int32 MaxCookieItems = 20;

        private readonly BaseRepo<DbContext, ViewedProduct> _viewedRepo;
        private readonly BaseRepo<DbContext, Product> _productsRepo;
        private readonly KeycloakUserService _keycloakUserService;
        private readonly RedisService<ViewedProduct> _redisService;
        private readonly CookieOptions _cookieOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ViewedProductService(DbContext context, CookieOptions cookieOptions, IHttpContextAccessor httpContextAccessor, KeycloakUserService keycloakUserService, RedisService<ViewedProduct> redisService) {
            _viewedRepo = new BaseRepo<DbContext, ViewedProduct>(context);
            _productsRepo = new BaseRepo<DbContext, Product>(context);
            _cookieOptions = cookieOptions;
            _redisService = redisService;
            _httpContextAccessor = httpContextAccessor;
            _keycloakUserService = keycloakUserService;

        }

        public async Task<IEnumerable<ViewedProduct>> GetViewedAsync(String? userId)
        {
            var user = userId != null ? await _keycloakUserService.GetUserAsync(userId) : null;
            return user != null ? await GetAuthenticatedAsync(userId!) : GetAnonymous();
        }

        public async Task TrackAsync(Guid productId, String? userId)
        {
            TrackInCookie(productId);

            var user = userId != null ? await _keycloakUserService.GetUserAsync(userId) : null;
            if (user == null) return;

            await PersistToDbAsync(userId!, productId);
            await _redisService.DeleteAsync(CacheKey(userId!));
        }

        public async Task MergeCookieIntoUserAsync(String userId)
        {
            var cookieItems = ReadCookieItems();
            if (!cookieItems.Any()) return;

            foreach (var item in cookieItems)
            {
                await PersistToDbAsync(userId, item.ProductId, item.WhenViewed);
            }

            Http.Response.Cookies.Delete(CookieKey);
            await _redisService.DeleteAsync(CacheKey(userId));
        }

        private IEnumerable<ViewedProduct> GetAnonymous()
        {
            return ReadCookieItems();
        }

        private async Task<IEnumerable<ViewedProduct>> GetAuthenticatedAsync(String userId)
        {
            var key = CacheKey(userId);

            if (await _redisService.IsRedisAvailable(key))
            {
                var cached = await _redisService.GetListAsync(key);
                if (cached.Any()) return cached.OrderByDescending(v => v.WhenViewed);
            }

            var all = await _viewedRepo.GetAllAsync();
            var records = all.Where(v => v.UserId == userId).OrderByDescending(v => v.WhenViewed).ToList();

            await _redisService.SetListAsync(key, records, CacheTtl);
            return records;
        }

        private async Task PersistToDbAsync(String userId, Guid? productId, DateTime? when = null)
        {
            if (productId == null) return;

            var all = await _viewedRepo.GetAllAsync();

            var existing = all.FirstOrDefault(v => v.UserId == userId && v.ProductId == productId);

            if (existing != null)
            {
                existing.WhenViewed = when ?? DateTime.UtcNow;
                _viewedRepo.Update(existing);
            }
            else
            {
                var record = new ViewedProduct
                {
                    UserId = userId,
                    ProductId = productId,
                    WhenViewed = when ?? DateTime.UtcNow
                };
                await _viewedRepo.AddAsync(record);
            }

            await _viewedRepo.SaveAsync();
        }


        private void TrackInCookie(Guid productId)
        {
            var items = ReadCookieEntries();

            items.RemoveAll(e => e.ProductId == productId);
            items.Insert(0, new CookieEntry(productId, DateTime.UtcNow));

            if (items.Count > MaxCookieItems)
            {
                items = items.Take(MaxCookieItems).ToList();
            }

            Http.Response.Cookies.Append(CookieKey, JsonSerializer.Serialize(items), _cookieOptions);
        }

        private List<ViewedProduct> ReadCookieItems()
        {
            return ReadCookieEntries()
                .Select(e => new ViewedProduct
                {
                    ProductId = e.ProductId,
                    WhenViewed = e.WhenViewed
                })
                .ToList();
        }
        private record CookieEntry(Guid ProductId, DateTime WhenViewed);
        private List<CookieEntry> ReadCookieEntries()
        {
            var raw = Http.Request.Cookies[CookieKey];
            if (String.IsNullOrEmpty(raw)) return new List<CookieEntry>();

            try { return JsonSerializer.Deserialize<List<CookieEntry>>(raw) ?? new List<CookieEntry>(); }
            catch { return new List<CookieEntry>(); }
        }

        private static String CacheKey(String userId) => $"viewed_products:{userId}";
        private HttpContext Http => _httpContextAccessor.HttpContext!;
    }
}
