using System.Text.Json.Serialization;

namespace WebStorePrototype.Server.Models.DTO_s
{
    public class KeycloakUser
    {
        [JsonPropertyName("id")]
        public String Id { get; set; } = null!;

        [JsonPropertyName("username")]
        public String Username { get; set; } = null!;

        [JsonPropertyName("Email")]
        public String Email { get; set; } = null!;
        
        [JsonPropertyName("firstname")]
        public String? FirstName { get; set; }
        
        [JsonPropertyName("lastname")]
        public String? LastName { get; set; }
        
        [JsonPropertyName("enabled")]
        public Boolean Enabled { get; set; }
        
        [JsonPropertyName("attributes")]
        public Dictionary<String, IEnumerable<String>>? Attributes { get; set; }
    }
}
