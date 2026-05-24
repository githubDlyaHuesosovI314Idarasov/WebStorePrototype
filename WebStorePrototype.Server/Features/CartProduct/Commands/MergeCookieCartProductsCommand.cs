using MediatR;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Features.CartProduct.Commands
{
    public record class MergeCookieCartProductsCommand(String userId) : IRequest;

    public class MergeCookieCartProductsHandler : IRequestHandler<MergeCookieCartProductsCommand>
    {
        private readonly ICartProductsService _service;
        public MergeCookieCartProductsHandler(ICartProductsService service)
        {
            _service = service;
        }
        public async Task Handle(MergeCookieCartProductsCommand request, CancellationToken cancellationToken)
        {
            await _service.MergeCookieIntoUserAsync(request.userId);
           
        }
    }

}
