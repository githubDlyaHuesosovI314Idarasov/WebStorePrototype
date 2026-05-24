using RabbitMQ.Client;

namespace WebStorePrototype.Server.Models.Base
{
    public interface IRabbitMqConnection
    {
        IConnection Connection { get; }
    }
}
