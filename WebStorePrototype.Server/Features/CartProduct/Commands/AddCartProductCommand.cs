using DAL.EF;
using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using WebStorePrototype.Server.Services;
using WebStorePrototype.Server.Services.Base;
using CartProductEntity = DAL.Models.CartProduct;

namespace WebStorePrototype.Server.Features.CartProduct.Commands
{
    public record class AddCartProductCommand(Guid ProductId, String? UserId) : IRequest<CartProductEntity>;

    public class AddCartProductHandler : IRequestHandler<AddCartProductCommand, CartProductEntity>
    {
        private readonly Repo<CartProductEntity> _cartRepo;
        private readonly Repo<Product> _productRepo;
        private readonly RedisService<CartProductEntity> _redisService;
        public AddCartProductHandler(WebStoreDBContext dbContext, HybridCache hybridCache, RedisService<CartProductEntity> redisService)
        {
            _cartRepo = new Repo<CartProductEntity>(dbContext, hybridCache);
            _productRepo = new Repo<Product>(dbContext, hybridCache);
            _redisService = redisService;
        }
    
        public async Task<CartProductEntity> Handle(AddCartProductCommand request, CancellationToken cancellationToken)
        {
            var cartProduct =  new CartProductEntity 
            { 
                ProductId = request.ProductId, 
                UserId = request.UserId, 
                Product = await _productRepo.GetAsync(request.ProductId) 
            };

            await _cartRepo.AddAsync(cartProduct, cancellationToken);
            await _cartRepo.SaveAsync();
            await _redisService.DeleteAsync("cartProduct:all");
            return cartProduct;
        }
    }

}
