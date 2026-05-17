using MediatR;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Features.ViewedProduct.Commands
{
    public record TrackViewedProductCommand(Guid productId, String? userId) : IRequest;

    public class TrackViewedProductHandler : IRequestHandler<TrackViewedProductCommand>
    {
        private readonly IViewedProductsService _service;
        public TrackViewedProductHandler(IViewedProductsService service)
        {
            _service = service;
        }

        public async Task Handle(TrackViewedProductCommand request, CancellationToken cancellationToken)
        {
            await _service.TrackAsync(request.productId, request.userId);
        }
    }
}
