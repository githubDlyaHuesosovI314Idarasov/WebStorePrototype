using AutoMapper;
using MediatR;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Features.FavoriteProduct.Commands
{
    public record class RemoveFavoriteProductCommand(Guid productId, String? userId) : IRequest;
    public class RemoveFavoriteProductHandler : IRequestHandler<RemoveFavoriteProductCommand>
    {
        private readonly IFavoriteProductsService _service;
        public RemoveFavoriteProductHandler(IFavoriteProductsService service) {
            _service = service;
        }

        public async Task Handle(RemoveFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            await _service.RemoveProductAsync(request.productId, request.userId);
        }
    }
}
