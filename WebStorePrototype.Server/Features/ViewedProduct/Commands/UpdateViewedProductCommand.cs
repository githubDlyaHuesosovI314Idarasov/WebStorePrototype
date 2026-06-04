using DAL.EF;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using StackExchange.Redis;
using WebStorePrototype.Server.Services;
using VProduct = DAL.Models.ViewedProduct;

namespace WebStorePrototype.Server.Features.ViewedProduct.Commands
{
    public record class UpdateViewedProductCommand(VProduct viewedProduct) : IRequest<Boolean>;

    public class UpdateViewedProductHandler : IRequestHandler<UpdateViewedProductCommand, Boolean>
    {
        private readonly Repo<VProduct> _viewedProductRepo;
        private readonly RedisService<VProduct> _redisService;

        public UpdateViewedProductHandler(WebStoreDBContext context, RedisService<VProduct> redisService, HybridCache cache)
        {
            _viewedProductRepo = new Repo<VProduct>(context, cache);
            _redisService = redisService;
        }
        public async Task<Boolean> Handle(UpdateViewedProductCommand request, CancellationToken cancellationToken)
        {
            RedisKey redisKey = $"viewedProduct:{request.viewedProduct.Id}";

            if(request.viewedProduct != null)
            {
                await _viewedProductRepo.UpdateAsync(request.viewedProduct);
                await _redisService.DeleteAsync(redisKey);
                await _redisService.DeleteAsync("viewedProduct:all");
                return true;
            }

            return false;
        }
    }
}
