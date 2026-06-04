using DAL.EF;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using WebStorePrototype.Server.Services;
using ComparedProductEntity = DAL.Models.ComparedProduct;

namespace WebStorePrototype.Server.Features.ComparedProduct.Commands
{
    public record class RemoveComparedProductCommand(Guid Id) : IRequest<Boolean>; 

    public class RemoveComparedProductHandler : IRequestHandler<RemoveComparedProductCommand, Boolean>
    {
        private readonly Repo<ComparedProductEntity> _comparedRepo;
        private readonly RedisService<ComparedProductEntity> _redisService;
        public RemoveComparedProductHandler(WebStoreDBContext context, HybridCache hybridCache, RedisService<ComparedProductEntity> redisService)
        {
            _comparedRepo = new Repo<ComparedProductEntity>(context, hybridCache);
            _redisService = redisService;
        }
        public async Task<Boolean> Handle(RemoveComparedProductCommand request, CancellationToken cancellationToken)
        {
            var comparedProduct = await _comparedRepo.GetAsync(request.Id);

            if (comparedProduct != null)
            {
                await _comparedRepo.DeleteAsync(comparedProduct);
                await _comparedRepo.SaveAsync();
                await _redisService.DeleteAsync($"comparedProduct:{request.Id}");
                await _redisService.DeleteAsync($"comparedProduct:all");
                return true;
            }

            return false;
        }
    }
}
