using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WebStorePrototype.Server.Services
{
    public class RabbbitMQService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbbitMQService()
        {
            
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
