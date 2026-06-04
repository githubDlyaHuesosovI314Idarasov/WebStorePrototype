using AutoMapper;
using DAL.EF;
using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using WebStorePrototype.Server.Services;
using WebStorePrototype.Server.Services.Base;
using FProduct = DAL.Models.FavoriteProduct;

namespace WebStorePrototype.Server.Features.FavoriteProduct.Commands
{
    public record class RemoveFavoriteProductCommand(Guid Id) : IRequest<Boolean>;
    public class RemoveFavoriteProductHandler : IRequestHandler<RemoveFavoriteProductCommand, Boolean>
    {
        private readonly Repo<FProduct> _favoriteRepo;
        private readonly RedisService<FProduct> _redisService;
        public RemoveFavoriteProductHandler(WebStoreDBContext context, HybridCache cache, RedisService<FProduct> redisService) {
            _favoriteRepo = new Repo<FProduct>(context, cache);
            _redisService = redisService;
        }

        public async Task<Boolean> Handle(RemoveFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            var favoriteProduct = await _favoriteRepo.GetAsync(request.Id);
            if (favoriteProduct != null)
            {
                await _favoriteRepo.DeleteAsync(favoriteProduct);
                await _favoriteRepo.SaveAsync();
                await _redisService.DeleteAsync($"favoriteProduct:{favoriteProduct.Id}");
                await _redisService.DeleteAsync($"favoriteProduct:all");
                return true;
            }
            return false;
        }
    }
}
