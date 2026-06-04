using DAL.EF;
using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using WebStorePrototype.Server.Services;
using FProduct = DAL.Models.FavoriteProduct;

namespace WebStorePrototype.Server.Features.FavoriteProduct.Commands
{
    public record class UpdateFavoriteProductCommand(FProduct favoriteProduct) : IRequest<Boolean>;

    public class UpdateFavoriteProductHandler : IRequestHandler<UpdateFavoriteProductCommand, Boolean>
    {
        private readonly Repo<FProduct> _favoriteRepo;
        private readonly RedisService<FProduct> _redisService;
        public UpdateFavoriteProductHandler(WebStoreDBContext context, HybridCache cache, RedisService<FProduct> redisService) {
        
            _favoriteRepo = new Repo<FProduct>(context, cache);
            _redisService = redisService;
        }

        public async Task<Boolean> Handle(UpdateFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            if(request.favoriteProduct != null) {
                await _favoriteRepo.UpdateAsync(request.favoriteProduct);
                await _favoriteRepo.SaveAsync();
                await _redisService.DeleteAsync($"favoriteProduct:{request.favoriteProduct.Id}");
                await _redisService.DeleteAsync($"favoriteProduct:all");
                return true;
            }
            return false;

        }
    }
}
