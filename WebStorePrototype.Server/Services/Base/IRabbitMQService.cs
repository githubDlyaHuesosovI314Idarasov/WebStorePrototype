namespace WebStorePrototype.Server.Services.Base
{
    public interface IRabbitMQService<T> where T : class
    {
        void PublishMessage<T>(T message);
    }
}
