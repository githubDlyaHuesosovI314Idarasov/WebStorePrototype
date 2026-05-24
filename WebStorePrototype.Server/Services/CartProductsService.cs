using DAL.Models;
using DAL.Repos;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using WebStorePrototype.Server.Models;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Services
{
    public class CartProductsService : ICartProductsService
    {
        private const String CookieKey = "cart_products";
        private readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(30);
        
        private readonly BaseRepo<DbContext, CartProduct> _cartRepo;
        private readonly KeycloakUserService _keycloakUserService;
        private readonly RedisService<CartProduct> _redisService;
        private readonly BaseRepo<DbContext, Product> _productsRepo;
        private readonly CookieOptions _cookieOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartProductsService(DbContext context, IHttpContextAccessor httpContextAccessor, KeycloakUserService keycloakUserService, CookieOptions cookieOptions, RedisService<CartProduct> redisService)
        {
            _cartRepo = new BaseRepo<DbContext, CartProduct>(context);
            _httpContextAccessor = httpContextAccessor;
            _keycloakUserService = keycloakUserService;
            _cookieOptions = cookieOptions;
            _redisService = redisService;
        }

        public async Task<IEnumerable<CartProduct>> GetProductsAsync(String? userId)
        {
            var user = userId != null ? await _keycloakUserService.GetUserAsync(userId) : null;
            return user != null ? await GetAuthenticatedAsync(userId!) : await GetAnonymousAsync();
        }

        public async Task TrackAsync(Guid productId, String? userId)
        {
            TrackInCookie(productId);
            
            var user = userId != null ? await _keycloakUserService.GetUserAsync(userId) : null;
            if (user != null) return;

            await PersistToDbAsync(userId, productId);
            await _redisService.DeleteAsync(CacheKey(userId!));
        }

        public async Task MergeCookieIntoUserAsync(String userId)
        {
            var cookieItems = ReadCookieItems();
            if (!cookieItems.Any()) return;

            foreach (var item in cookieItems)
            {
                await PersistToDbAsync(userId, item.ProductId);
            }

            Http.Response.Cookies.Delete(CookieKey);
            await _redisService.DeleteAsync(CacheKey(userId));
        }
        private async Task PersistToDbAsync(String? userId, Guid productId)
        {
            var all = await _cartRepo.GetAllAsync();
            var existing = all.FirstOrDefault(v => v.UserId == userId && v.ProductId == productId);

            if (existing != null)
            {
                _cartRepo.Update(existing);
            }
            else
            {
                var record = new CartProduct
                {
                    UserId = userId,
                    ProductId = productId,
                    Product = await _productsRepo.GetAsync(productId)
                };
                await _cartRepo.AddAsync(record);
            }

            await _cartRepo.SaveAsync();
        }

        private void TrackInCookie(Guid productId)
        {
            var items = ReadCookieEntries().ToList();
            items.RemoveAll(i => i.ProductId == productId);
            items.Insert(0, new CartProductCookieEntry(productId));

            Http.Response.Cookies.Append(CookieKey, JsonSerializer.Serialize(items), _cookieOptions);
        }

        private async Task<IEnumerable<CartProduct>> GetAuthenticatedAsync(String userId)
        {
            var key = CacheKey(userId);

            if(await _redisService.IsRedisAvailable(key))
            {
                var cached = await _redisService.GetListAsync(key);
                if(cached.Any()) return cached;
            }

            var all = await _cartRepo.GetAllAsync();
            var records = all.Where(x => x.UserId == userId);

            await _redisService.SetListAsync(key, records, CacheTtl);
            return records;
        }

        private async Task<IEnumerable<CartProduct>> GetAnonymousAsync()
        {
            return ReadCookieItems();
        }

        private IEnumerable<CartProduct> ReadCookieItems()
        {
            return ReadCookieEntries().Select( x =>  new CartProduct
            {
                ProductId = x.ProductId,
            });
        }
        private IEnumerable<CartProductCookieEntry> ReadCookieEntries()
        {
            var raw = Http.Request.Cookies[CookieKey];
            if (String.IsNullOrEmpty(raw)) return Enumerable.Empty<CartProductCookieEntry>();

            try{ return JsonSerializer.Deserialize<IEnumerable<CartProductCookieEntry>>(raw) ?? Enumerable.Empty<CartProductCookieEntry>(); }
            catch { return new List<CartProductCookieEntry>(); }
           
        }
        private static String CacheKey(String userId) => $"cart_products:{userId}";

        public async Task AddProductAsync(Guid productId, String? userId)
        {
            List<CartProduct> cartProducts = (List<CartProduct>) await GetProductsAsync(userId);
            cartProducts.Add(new CartProduct
            {
                ProductId = productId,
                UserId = userId
            });

            await _redisService.DeleteAsync(CacheKey(userId!));
        }

        public async Task RemoveProductAsync(Guid productId, String? userId)
        {
            List<CartProduct> cartProducts = (List<CartProduct>) await GetProductsAsync(userId);
            CartProduct? product = cartProducts.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
              cartProducts.Remove(product);
              await _redisService.DeleteAsync(CacheKey(userId!));
            }
           
            return;
        }

        private HttpContext Http => _httpContextAccessor.HttpContext!;
    }
}
