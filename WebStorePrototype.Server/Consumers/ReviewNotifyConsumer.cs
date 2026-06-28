using Contracts;
using MassTransit;

namespace WebStorePrototype.Server.Consumers
{
    public class ReviewNotifyConsumer : IConsumer<ReviewNotify>
    {
        private readonly ILogger _logger;
        public ReviewNotifyConsumer(ILogger logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<ReviewNotify> context)
        {
            _logger.LogInformation("Received ReviewNotify: {Title}", context.Message.Title);
            return Task.CompletedTask;
        }
    }
}
