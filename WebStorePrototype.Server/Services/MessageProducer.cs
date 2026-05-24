using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Services.Base;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;

namespace WebStorePrototype.Server.Services
{
    public class MessageProducer : IMessageProducer
    {
        private readonly IRabbitMqConnection _rabbitMqConnection;
        public MessageProducer(IRabbitMqConnection rabbitMqConnection) {
            _rabbitMqConnection = rabbitMqConnection;
        }

        public async Task SendMessage<T>(T message)
        {
            using var channel = await _rabbitMqConnection.Connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "message_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchange: String.Empty, routingKey: "message_queue", body: body);
        }
    }
}
