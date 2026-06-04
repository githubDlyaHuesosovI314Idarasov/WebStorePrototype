using AutoMapper;
using DAL.EF;
using DAL.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using StackExchange.Redis;
using WebStorePrototype.Server.Models.DTO_s;
using WebStorePrototype.Server.Services;
using ComparedProductEntity = DAL.Models.ComparedProduct;

namespace WebStorePrototype.Server.Features.ComparedProduct.Queries
{
    public record class GetComparedProductQuery(Guid Id) : IRequest<ComparedProductDTO>;

    public class GetComparedProductHandler : IRequestHandler<GetComparedProductQuery, ComparedProductDTO>
    {
        private readonly Repo<ComparedProductEntity> _comparedRepo;
        private readonly RedisService<ComparedProductEntity> _redisService;
        private readonly IMapper _mapper;

        public GetComparedProductHandler(WebStoreDBContext context, HybridCache cache, RedisService<ComparedProductEntity> redisService, IMapper mapper)
        {
            _comparedRepo = new Repo<ComparedProductEntity>(context, cache);
            _redisService = redisService;
            _mapper = mapper;
        }

        public async Task<ComparedProductDTO> Handle(GetComparedProductQuery request, CancellationToken cancellationToken)
        {
            RedisKey redisKey = $"comparedProduct:{request.Id}";
            if (await _redisService.IsRedisAvailable(redisKey))
            {
                return _mapper.Map<ComparedProductDTO>(await _redisService.GetAsync(redisKey));
            }

            return _mapper.Map<ComparedProductDTO>(await _comparedRepo.GetAsync(request.Id));
        }
    }

}
