using Contracts;
using MassTransit;
using System.Collections.Concurrent;

namespace WebStorePrototype.Server.Consumers
{
    public class DeliveryNotifyConsumer : IConsumer<DeliveryNotify>
    {
        private readonly ILogger _logger;
        public DeliveryNotifyConsumer(ILogger logger) 
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<DeliveryNotify> context)
        {
            _logger.LogInformation("Received DeliveryNotify: {Title} {WaybillNumber} {Status}", context.Message.Title, context.Message.WaybillNumber, context.Message.Status);
            return Task.CompletedTask;
        }
    }
}
