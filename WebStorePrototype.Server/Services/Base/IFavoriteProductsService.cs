using DAL.Models;

namespace WebStorePrototype.Server.Services.Base
{
    public interface IFavoriteProductsService
    {
        Task<IEnumerable<Product>> GetProductsAsync(String? userId);

        Task AddProductAsync(Guid productId, String? userId);

        Task RemoveProductAsync(Guid productId, String? userId);

        Task MergeCookieIntoUserAsync(String service);

    }
}
