using DAL.Models;

namespace WebStorePrototype.Server.Services.Base
{
    public interface IViewedProductsService
    { 
        Task<IEnumerable<ViewedProduct>> GetViewedAsync(String? userId);

        Task TrackAsync(Guid productId, String? userId);

        Task MergeCookieIntoUserAsync(String userId);
    }
}
