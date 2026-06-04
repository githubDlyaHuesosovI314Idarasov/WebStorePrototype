using DAL.EF;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using WebStorePrototype.Server.Services;
using VProduct = DAL.Models.ViewedProduct;

namespace WebStorePrototype.Server.Features.ViewedProduct.Commands
{
    public record class RemoveViewedProductCommand(Guid id) : IRequest<Boolean>;

    public class RemoveViewedProductHandler : IRequestHandler<RemoveViewedProductCommand, Boolean>
    {
        private readonly Repo<VProduct> _viewedRepo;
        private readonly RedisService<VProduct> _redisService;
        public RemoveViewedProductHandler(WebStoreDBContext dBContext, HybridCache cache, RedisService<VProduct> redisService)
        {
            _viewedRepo = new Repo<VProduct>(dBContext, cache);
            _redisService = redisService;
        }
        public async Task<Boolean> Handle(RemoveViewedProductCommand request, CancellationToken cancellationToken)
        {
           var viewedProduct = await _viewedRepo.GetAsync(request.id);
            if (viewedProduct != null)
            {
                await _viewedRepo.DeleteAsync(viewedProduct);
                await _viewedRepo.SaveAsync();
                await _redisService.DeleteAsync($"viewedproduct:{viewedProduct.Id}");
                await _redisService.DeleteAsync($"viewedproduct:all");
                return true;
            }
            return false;
        }
    }
}
