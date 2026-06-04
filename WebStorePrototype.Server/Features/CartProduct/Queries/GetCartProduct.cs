using AutoMapper;
using DAL.EF;
using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using StackExchange.Redis;
using WebStorePrototype.Server.Models.DTO_s;
using WebStorePrototype.Server.Services;
using WebStorePrototype.Server.Services.Base;
using CartProductEntity = DAL.Models.CartProduct;

namespace WebStorePrototype.Server.Features.CartProduct.Queries
{
    public record class GetCartProductQuery(Guid Id) : IRequest<CartProductDTO>;

    public class GetCartProductsHandler : IRequestHandler<GetCartProductQuery, CartProductDTO>
    {
        private readonly Repo<CartProductEntity> _cartRepo;
        private readonly RedisService<CartProductEntity> _redisService;
        private readonly IMapper _mapper;
        public GetCartProductsHandler(WebStoreDBContext dbContext, HybridCache cache, IMapper mapper, RedisService<CartProductEntity> redisService)
        {
            _cartRepo = new Repo<CartProductEntity>(dbContext, cache);
            _mapper = mapper;
            _redisService = redisService;
        }
        public async Task<CartProductDTO> Handle(GetCartProductQuery request, CancellationToken cancellationToken)
        {
            RedisKey redisKey = $"cartProduct:{request.Id}";
            if (await _redisService.IsRedisAvailable(redisKey))
            {
                return _mapper.Map<CartProductDTO>(await _redisService.GetAsync(redisKey)) ;
            }
            
            return _mapper.Map<CartProductDTO>(await _cartRepo.GetAsync(request.Id));
        }
    }
}
