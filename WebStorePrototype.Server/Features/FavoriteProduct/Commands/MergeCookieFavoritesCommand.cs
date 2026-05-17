using MediatR;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Features.FavoriteProduct.Commands
{
    public record class MergeCookieFavoritesCommand(String userId) : IRequest;

    public class MergeCookieFavoritesHander : IRequestHandler<MergeCookieFavoritesCommand>
    {
        private readonly IFavoriteProductsService _service;
       
        public MergeCookieFavoritesHander(IFavoriteProductsService service)
        {
            _service = service;
        }

        public async Task Handle(MergeCookieFavoritesCommand request, CancellationToken cancellationToken)
        {
            await _service.MergeCookieIntoUserAsync(request.userId);
        }
    };
}