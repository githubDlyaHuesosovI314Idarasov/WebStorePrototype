using AutoMapper;
using MediatR;
using WebStorePrototype.Server.Models.DTO_s;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Features.FavoriteProduct.Queries
{
    public record class GetFavoriteProducyQuery(String? userId) : IRequest<IEnumerable<FavoriteProductDTO>>;
    public class GetFavoriteProductHandler : IRequestHandler<GetFavoriteProducyQuery, IEnumerable<FavoriteProductDTO>>
    {
        private readonly IFavoriteProductsService _service;
        private readonly IMapper _mapper;
        public GetFavoriteProductHandler(IFavoriteProductsService service, IMapper mapper) {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FavoriteProductDTO>> Handle(GetFavoriteProducyQuery request, CancellationToken cancellationToken)
        {
            var products = await _service.GetProductsAsync(request.userId);
            return _mapper.Map<IEnumerable<FavoriteProductDTO>>(products);
        }
    }
}
