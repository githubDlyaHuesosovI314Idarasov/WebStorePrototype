using AutoMapper;
using MediatR;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Features.CartProduct.Commands
{
    public record class RemoveCartProductCommand(Guid ProductId, String? UserId) : IRequest;

    public class RemoveCartProductHandler : IRequestHandler<RemoveCartProductCommand>
    {
        private readonly ICartProductsService _service;
        public RemoveCartProductHandler(ICartProductsService service, IMapper mapper)
        {
            _service = service;
        }
    
        public async Task Handle(RemoveCartProductCommand request, CancellationToken cancellationToken)
        {
            await _service.RemoveProductAsync(request.ProductId, request.UserId);
        }
    }

}
