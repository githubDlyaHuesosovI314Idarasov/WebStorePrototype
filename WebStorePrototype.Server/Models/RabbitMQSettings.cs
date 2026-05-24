using RabbitMQ.Client;

namespace WebStorePrototype.Server.Models
{
    public class RabbitMQSettings
    {
        public String HostName { get; set; } = null!;
        public String VirtualHost { get; set; } = null!;
        public Int32 Port { get; set; } 
        public String UserName { get; set; } = null!;
        public String Password { get; set; } = null!;

        public virtual IConnectionFactory GetConnectionFactory()
        {
            return new ConnectionFactory
            {
                HostName = this.HostName,
                VirtualHost = this.VirtualHost,
                Port = this.Port,
                UserName = this.UserName,
                Password = this.Password,
                AutomaticRecoveryEnabled = true
                
            };
        }

    }
}
