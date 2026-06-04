using AutoMapper;
using DAL.EF;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using WebStorePrototype.Server.Services;
using WebStorePrototype.Server.Services.Base;
using CartProductEntity = DAL.Models.CartProduct;

namespace WebStorePrototype.Server.Features.CartProduct.Commands
{
    public record class RemoveCartProductCommand(Guid Id) : IRequest<Boolean>;

    public class RemoveCartProductHandler : IRequestHandler<RemoveCartProductCommand, Boolean>
    {
        private readonly Repo<CartProductEntity> _cartRepo;
        private readonly RedisService<CartProductEntity> _redisService;
        public RemoveCartProductHandler(WebStoreDBContext dbContext, HybridCache hybridCache, RedisService<CartProductEntity> redisService)
        {
            _cartRepo = new Repo<CartProductEntity>(dbContext, hybridCache);
            _redisService = redisService;
        }
    
        public async Task<Boolean> Handle(RemoveCartProductCommand request, CancellationToken cancellationToken)
        {
            var cartProduct = await _cartRepo.GetAsync(request.Id);
            if (cartProduct != null) 
            {
                await _cartRepo.DeleteAsync(cartProduct);
                await _cartRepo.SaveAsync();
                await _redisService.DeleteAsync($"cartProduct:{cartProduct.Id}");
                await _redisService.DeleteAsync($"cartProduct:all");
                return true;
            }

            return false;
        }
    }

}
