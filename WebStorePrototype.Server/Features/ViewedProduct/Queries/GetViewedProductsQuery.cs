using AutoMapper;
using MediatR;
using WebStorePrototype.Server.Models.DTO_s;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Features.ViewedProduct.Queries
{

    public record GetViewedProductsQuery(String? userId) : IRequest<IEnumerable<ViewedProductDTO>>;
    public class GetViewedProductsHandler : IRequestHandler<GetViewedProductsQuery, IEnumerable<ViewedProductDTO>>
    {
        private readonly IViewedProductsService _service; 
        private readonly IMapper _mapper;
        
        public GetViewedProductsHandler(IViewedProductsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ViewedProductDTO>> Handle(GetViewedProductsQuery request, CancellationToken cancellationToken)
        {
            var viewed = await _service.GetViewedAsync(request.userId);
            return _mapper.Map<IEnumerable<ViewedProductDTO>>(viewed);
        }
    }
}
