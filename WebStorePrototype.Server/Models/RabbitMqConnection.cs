using RabbitMQ.Client;
using WebStorePrototype.Server.Models.Base;

namespace WebStorePrototype.Server.Models
{
    public class RabbitMqConnection : IRabbitMqConnection, IDisposable
    {
        private IConnection? _connection;
        public IConnection Connection => _connection!;
    
        public RabbitMqConnection(RabbitMQSettings settings)
        {
            InitializeConnection(settings).GetAwaiter().GetResult();
        }
        private async Task InitializeConnection(RabbitMQSettings settings)
        {
            var factory = settings.GetConnectionFactory();
            _connection = await factory.CreateConnectionAsync();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
