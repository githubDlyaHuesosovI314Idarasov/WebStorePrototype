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
    public record AddFavoriteProductCommand(Guid productId, String? userId) : IRequest<FProduct>;

    public class AddFavoriteProductHandler : IRequestHandler<AddFavoriteProductCommand, FProduct>
    {
        private Repo<FProduct> _favoritesRepo; 
        private Repo<Product> _productsRepo;
        private RedisService<FProduct> _redisService;

        public AddFavoriteProductHandler(WebStoreDBContext context, RedisService<FProduct> redisService, HybridCache cache) 
        {
            _favoritesRepo = new Repo<FProduct>(context, cache);
            _productsRepo = new Repo<Product>(context, cache);
            _redisService = redisService;
        }

        public async Task<FProduct> Handle(AddFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            var favoriteProduct = new FProduct
            {
                ProductId = request.productId,
                UserId = request.userId,
                Product = await _productsRepo.GetAsync(request.productId)
            };
            await _favoritesRepo.AddAsync(favoriteProduct, cancellationToken);
            await _favoritesRepo.SaveAsync();
            await _redisService.DeleteAsync($"favoriteProduct:all");
            return favoriteProduct;
        }
    }
}
