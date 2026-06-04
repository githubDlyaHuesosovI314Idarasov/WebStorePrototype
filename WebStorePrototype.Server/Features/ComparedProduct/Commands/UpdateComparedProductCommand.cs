using DAL.EF;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using StackExchange.Redis;
using WebStorePrototype.Server.Services;
using ComparedProductEntity = DAL.Models.ComparedProduct;

namespace WebStorePrototype.Server.Features.ComparedProduct.Commands
{
    public record class UpdateComparedProductCommand(ComparedProductEntity ComparedProduct) : IRequest<Boolean>;

    public record class UpdateComparedProductHandler : IRequestHandler<UpdateComparedProductCommand, Boolean>
    {
        private readonly Repo<ComparedProductEntity> _comparedRepo;
        private readonly RedisService<ComparedProductEntity> _redisService;
        public UpdateComparedProductHandler(WebStoreDBContext context, HybridCache hybridCache, RedisService<ComparedProductEntity> redisService)
        {
            _comparedRepo = new Repo<ComparedProductEntity>(context, hybridCache);
            _redisService = redisService;
        }

        public async Task<Boolean> Handle(UpdateComparedProductCommand request, CancellationToken cancellationToken)
        {
            RedisKey redisKey = $"comparedProduct:{request.ComparedProduct.Id}";

            if (request.ComparedProduct != null)
            {
                await _comparedRepo.UpdateAsync(request.ComparedProduct);
                await _comparedRepo.SaveAsync();
                await _redisService.DeleteAsync(redisKey);
                await _redisService.DeleteAsync("comparedProduct:all");

                return true;
            }

            return false;
        }
    }
}
