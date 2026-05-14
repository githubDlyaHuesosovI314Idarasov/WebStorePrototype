using System.Text.Json;
using WebStorePrototype.Server.Models;

namespace WebStorePrototype.Server.Services
{
    public class KeycloakAdminTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly KeycloakConfiguration _keycloakConfiguration;

        private String? _cachedToken;
        private DateTime _tokenExpiry = DateTime.MinValue;

        public KeycloakAdminTokenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _keycloakConfiguration = new KeycloakConfiguration();
        }

        public async Task<String> GetTokenAsync()
        {
            if (_cachedToken != null && DateTime.UtcNow < _tokenExpiry)
            {                
                 return _cachedToken;   
            }

            var tokenUrl = $"{_keycloakConfiguration.BaseUrl}/realms/{_keycloakConfiguration.Realm}/protocol/openid-connect/token";

            var body = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<String, String>("grant_type", "client_credentials"),
                new KeyValuePair<String, String>("client_id", _keycloakConfiguration.ClientId),
                new KeyValuePair<String, String>("client_secret", _keycloakConfiguration.SecretKey)
            });

            var response = await _httpClient.PostAsync(tokenUrl, body);
        
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            _cachedToken = json.GetProperty("access_token").GetString();
            var expiresIn = json.GetProperty("expires_in").GetInt32();
            _tokenExpiry = DateTime.UtcNow.AddSeconds(expiresIn - 30);

            return _cachedToken!;
        }



    }
}
