using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace WebStorePrototype.Server.Models
{
    public class SimpleMessageHandler : BaseMessangerHandler<SimpleMessageHandler>
    {
        private readonly RabbitMQSettings _rabbitmqSettings;
        public SimpleMessageHandler(IOptions<RabbitMQSettings> settings, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _rabbitmqSettings = settings.Value;
            _connectionFactory = _rabbitmqSettings.GetConnectionFactory();

        }
        protected override async Task RegisterSubscribers()
        {
            if (_rabbitmqSettings == null || _channel == null) {
                return;
            }   

            await RegisterMessageHandlers();
        }

        protected override async Task RegisterMessageHandlers()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += OnMessageReceived;

            await _channel.BasicConsumeAsync("message_queue", true,"", false, false, null, consumer);

        }

        protected override async Task ProcessMessage(IServiceScope scope, String message)
        {
            try
            {
                var simpleMessage = JsonSerializer.Deserialize<SimpleMessage>(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }

        }
    }

}
