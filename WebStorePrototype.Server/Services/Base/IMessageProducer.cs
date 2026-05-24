namespace WebStorePrototype.Server.Services.Base
{
    public interface IMessageProducer
    {
        Task SendMessage<T>(T message);
    }
}
