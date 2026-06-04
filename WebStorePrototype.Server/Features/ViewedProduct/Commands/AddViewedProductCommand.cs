using DAL.EF;
using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using WebStorePrototype.Server.Services;
using VProduct = DAL.Models.ViewedProduct;

namespace WebStorePrototype.Server.Features.ViewedProduct.Commands
{
    public record class AddViewedProductCommand(Guid productId, String? userId) : IRequest<VProduct>;

    public class AddViewedProductHandler : IRequestHandler<AddViewedProductCommand, VProduct>
    {
        private readonly Repo<VProduct> _viewedRepo;
        private readonly Repo<Product> _productRepo;
        private readonly RedisService<VProduct> _redisService;
        public AddViewedProductHandler(WebStoreDBContext context, HybridCache cache, RedisService<VProduct> redisService)
        {
            _viewedRepo = new Repo<VProduct>(context, cache);
            _productRepo = new Repo<Product>(context, cache);
            _redisService = redisService;
        }
        public async Task<VProduct> Handle(AddViewedProductCommand request, CancellationToken cancellationToken)
        {
            var viewedProduct = new VProduct
            {
                ProductId = request.productId,
                Product = await _productRepo.GetAsync(request.productId),
                UserId = request.userId
            };

            await _viewedRepo.AddAsync(viewedProduct, cancellationToken);
            await _viewedRepo.SaveAsync();
            await _redisService.DeleteAsync("viewedProduct:all");
            return viewedProduct;
        }
    }

}
