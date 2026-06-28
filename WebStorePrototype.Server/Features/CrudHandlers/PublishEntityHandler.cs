using DAL;
using MassTransit;
using MediatR;
using WebStorePrototype.Server.Features.Base;

namespace WebStorePrototype.Server.Features.CrudHandlers
{
    public class PublishEntityHandler<T> : IRequestHandler<PublishCommand<T>> where T : Entity
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public PublishEntityHandler(IPublishEndpoint publishEndpoint) 
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(PublishCommand<T> request, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(request.Entity, cancellationToken);
        }
    }
}
