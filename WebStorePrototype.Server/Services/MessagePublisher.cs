using MassTransit;
using WebStorePrototype.Server.Models.Events.Order;

namespace WebStorePrototype.Server.Services
{
    public class MessagePublisher : BackgroundService
    {
        private readonly IBus _bus;
        public MessagePublisher(IBus bus)
        {
            _bus = bus;
           
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _bus.Publish(new OrderCreated
            {
                Id = Guid.NewGuid(),
                UserId = "", // TODO: Add userID to Order
                OrderNumber = "ORD-123456",
                TotalAmount = 991212,
                WhenCreated = DateTime.UtcNow
            }, stoppingToken);


        }
    }
}
