using AutoMapper;
using DAL.Models;
using DAL.Repos;
using MassTransit;
using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Models.Events;
using WebStorePrototype.Server.Models.Events.Order;

namespace WebStorePrototype.Server.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly ILogger _logger;
        public OrderCreatedConsumer(ILogger logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderCreated> context)
        {
            _logger.LogInformation("Received OrderCreated event for OrderId: {OrderId} WhenCreated: {WhenCreated}", 
                context.Message.Id,
                context.Message.WhenCreated);
            return Task.CompletedTask;
        }
    }
}
