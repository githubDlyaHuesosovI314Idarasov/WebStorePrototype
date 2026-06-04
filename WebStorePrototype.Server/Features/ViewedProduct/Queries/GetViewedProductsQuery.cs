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
using VProduct = DAL.Models.ViewedProduct;

namespace WebStorePrototype.Server.Features.ViewedProduct.Queries
{

    public record GetViewedProductsQuery(Guid id) : IRequest<ViewedProductDTO>;
    public class GetViewedProductsHandler : IRequestHandler<GetViewedProductsQuery, ViewedProductDTO>
    {
        private readonly Repo<VProduct> _repo;
        private readonly RedisService<VProduct> _redisService;
        private readonly IMapper _mapper;
        
        public GetViewedProductsHandler(WebStoreDBContext dbContext, HybridCache hybridCache, IMapper mapper, RedisService<VProduct> redisService)
        {
            _repo = new Repo<VProduct>(dbContext, hybridCache);
            _mapper = mapper;
            _redisService = redisService;
        }

        public async Task<ViewedProductDTO> Handle(GetViewedProductsQuery request, CancellationToken cancellationToken)
        {
            RedisKey redisKey = $"viewedProduct:{request.id}";
            if(await _redisService.IsRedisAvailable(redisKey))
            {
               return _mapper.Map<ViewedProductDTO>(await _redisService.GetAsync(redisKey));
            }


            return _mapper.Map<ViewedProductDTO>(await _repo.GetAsync(request.id));
        }
    }
}
