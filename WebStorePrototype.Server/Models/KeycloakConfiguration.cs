using Microsoft.Extensions.Primitives;

namespace WebStorePrototype.Server.Models
{
    public class KeycloakConfiguration 
    {
        public String BaseUrl { get; set; } = null!;
        public String Realm { get; set; } = null!;
        public String ClientId { get; set; } = null!;
        public String SecretKey { get; set; } = null!;

    }
}
