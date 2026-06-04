using DAL.EF;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using StackExchange.Redis;
using WebStorePrototype.Server.Services;
using CartProductEntity = DAL.Models.CartProduct;

namespace WebStorePrototype.Server.Features.CartProduct.Commands
{
    public record class UpdateCartProductCommand(CartProductEntity CartProduct) : IRequest<Boolean>;
    public class UpdateCartProductHandler : IRequestHandler<UpdateCartProductCommand, Boolean>
    {
        private readonly Repo<CartProductEntity> _cartProductsRepo;
        private readonly RedisService<CartProductEntity> _redisService;
        public UpdateCartProductHandler(WebStoreDBContext dbContext, RedisService<CartProductEntity> redisService, HybridCache cache)
        {
            _cartProductsRepo = new Repo<CartProductEntity>(dbContext, cache);
            _redisService = redisService;
        }
        public async Task<Boolean> Handle(UpdateCartProductCommand request, CancellationToken cancellationToken)
        {
            RedisKey redisKey = $"cartProduct:{request.CartProduct.Id}";

            if (request.CartProduct != null) {

                await _cartProductsRepo.UpdateAsync(request.CartProduct);
                await _cartProductsRepo.SaveAsync();
                await _redisService.DeleteAsync(redisKey);
                await _redisService.DeleteAsync("cartProduct:all");

                return true;
            }

            return false;
        }
    }
}
