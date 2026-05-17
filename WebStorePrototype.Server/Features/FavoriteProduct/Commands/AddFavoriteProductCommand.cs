using AutoMapper;
using MediatR;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Features.FavoriteProduct.Commands
{
    public record AddFavoriteProductCommand(Guid productId, String? userId) : IRequest;

    public class AddFavoriteProductHandler : IRequestHandler<AddFavoriteProductCommand>
    {
        private readonly IFavoriteProductsService _service;
        public AddFavoriteProductHandler(IFavoriteProductsService service) 
        {
            _service = service;
        }

        public async Task Handle(AddFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            await _service.AddProductAsync(request.productId, request.userId);
        }
    }
}
