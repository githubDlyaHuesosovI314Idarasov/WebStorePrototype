using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace WebStorePrototype.Server.Models
{
    public class BaseMessangerHandler<T> : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        protected IConnectionFactory _connectionFactory;
        protected IConnection _connection;
        protected IChannel _channel;

        public BaseMessangerHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await RegisterSubscribers();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_connection != null)
            {
                await _connection.CloseAsync();
            }
        }

        protected async Task InitializeRabbitMQConnection()
        {
            _connection = await _connectionFactory.CreateConnectionAsync();
            await CreateConnection();
        }

        protected virtual async Task OnMessageReceived(Object sender, BasicDeliverEventArgs ea)
        {
            String message = Encoding.UTF8.GetString(ea.Body.ToArray());

            using var scope = _serviceProvider.CreateScope();
            await ProcessMessage(scope, message);

            await _channel.BasicAckAsync(ea.DeliveryTag, false);
        }

        private async Task CreateConnection()
        {
            if (_connection == null) 
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action.");
            }

            if (!_connection.IsOpen)
            {
                throw new InvalidOperationException("No RabbitMQ connections are open to perform this action.");
            }
            
            _channel?.Dispose();
            _channel = await _connection.CreateChannelAsync();
        }

        protected virtual async Task RegisterSubscribers()
        {
            throw new NotImplementedException();
        }

        protected virtual async Task RegisterMessageHandlers()
        {
            throw new NotImplementedException();
        }

        protected virtual async Task ProcessMessage(IServiceScope scope, String message)
        {
            throw new NotImplementedException();
        }
    }
}
