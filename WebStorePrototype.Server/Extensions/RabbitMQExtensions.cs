using WebStorePrototype.Server.Models;
using WebStorePrototype.Server.Models.Base;

namespace WebStorePrototype.Server.Extensions
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
            services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));
            services.AddHostedService<SimpleMessageHandler>();
            return services;
        }
    }
}
