using DAL.EF;
using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using WebStorePrototype.Server.Services;
using ComparedProductEnitity = DAL.Models.ComparedProduct;

namespace WebStorePrototype.Server.Features.ComparedProduct.Commands
{
    public record class AddComparedProductCommand(Guid ProductId, String? UserId) : IRequest<ComparedProductEnitity>;

    public class AddComparedProductHandler : IRequestHandler<AddComparedProductCommand, ComparedProductEnitity>
    {
        private readonly Repo<ComparedProductEnitity> _comparedRepo;
        private readonly Repo<Product> _productRepo;
        private readonly RedisService<ComparedProductEnitity> _redisService;
        public AddComparedProductHandler(WebStoreDBContext context, HybridCache hybridCache , RedisService<ComparedProductEnitity> redisService)
        {
            _comparedRepo = new Repo<ComparedProductEnitity>(context, hybridCache);
            _productRepo = new Repo<Product>(context, hybridCache);
            _redisService = redisService;
        }

        public async Task<ComparedProductEnitity> Handle(AddComparedProductCommand request, CancellationToken cancellationToken)
        {
            var comparedProduct = new ComparedProductEnitity
            {
                ProductId = request.ProductId,
                Product = await _productRepo.GetAsync(request.ProductId),
                UserId = request.UserId,
            };

            await _comparedRepo.AddAsync(comparedProduct, cancellationToken);
            await _comparedRepo.SaveAsync();
            await _redisService.DeleteAsync($"comparedProduct:all");

            return comparedProduct;
        }
    }
}
