using MediatR;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Features.CartProduct.Commands
{
    public record class AddCartProductCommand(Guid ProductId, String? UserId) : IRequest;

    public class AddCartProductHandler : IRequestHandler<AddCartProductCommand>
    {
        private readonly ICartProductsService _service;
        public AddCartProductHandler(ICartProductsService service)
        {
            _service = service;
        }
    
        public async Task Handle(AddCartProductCommand request, CancellationToken cancellationToken)
        {
            await _service.AddProductAsync(request.ProductId, request.UserId);
        }
    }

}
