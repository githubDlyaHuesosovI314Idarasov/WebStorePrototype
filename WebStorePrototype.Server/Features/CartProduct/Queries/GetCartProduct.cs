using AutoMapper;
using MediatR;
using WebStorePrototype.Server.Models.DTO_s;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Features.CartProduct.Queries
{
    public record class GetCartProductQuery(String? userId) : IRequest<IEnumerable<CartProductDTO>>;

    public class GetCartProductsHandler : IRequestHandler<GetCartProductQuery, IEnumerable<CartProductDTO>>
    {
        private readonly ICartProductsService _service;
        private readonly IMapper _mapper;
        public GetCartProductsHandler(ICartProductsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CartProductDTO>> Handle(GetCartProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _service.GetProductsAsync(request.userId);
            return _mapper.Map<IEnumerable<CartProductDTO>>(products);
        }
    }
}
