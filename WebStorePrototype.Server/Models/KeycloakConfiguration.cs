using Microsoft.Extensions.Primitives;

namespace WebStorePrototype.Server.Models
{
    public class KeycloakConfiguration 
    {
        public String BaseUrl { get; set; }
        public String Realm { get; set; }
        public String ClientId { get; set; }
        public String SecretKey { get; set; }

    }
}
