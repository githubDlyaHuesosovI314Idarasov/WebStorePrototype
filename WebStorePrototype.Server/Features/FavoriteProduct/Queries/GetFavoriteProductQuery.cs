using AutoMapper;
using DAL.EF;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using StackExchange.Redis;
using WebStorePrototype.Server.Models.DTO_s;
using WebStorePrototype.Server.Services;
using WebStorePrototype.Server.Services.Base;
using FProduct = DAL.Models.FavoriteProduct;

namespace WebStorePrototype.Server.Features.FavoriteProduct.Queries
{
    public record class GetFavoriteProductQuery(Guid favoriteProductId) : IRequest<FavoriteProductDTO>;
    public class GetFavoriteProductHandler : IRequestHandler<GetFavoriteProductQuery, FavoriteProductDTO>
    {
        private readonly Repo<FProduct> _favoriteRepo;
        private readonly RedisService<FProduct> _redisService;
        private readonly IMapper _mapper;
        public GetFavoriteProductHandler(WebStoreDBContext context, HybridCache cache, IMapper mapper, RedisService<FProduct> redisService) {
            _favoriteRepo = new Repo<FProduct>(context, cache);
            _mapper = mapper;
            _redisService = redisService;
        }

        public async Task<FavoriteProductDTO> Handle(GetFavoriteProductQuery request, CancellationToken cancellationToken)
        {
            RedisKey redisKey = $"favoriteProduct:{request.favoriteProductId}";
            if (await _redisService.IsRedisAvailable(redisKey)) { 
                
                return  _mapper.Map<FavoriteProductDTO>(await _redisService.GetAsync(redisKey));
            }

            return _mapper.Map<FavoriteProductDTO>(await _favoriteRepo.GetAsync(request.favoriteProductId));
        }
    }
}
