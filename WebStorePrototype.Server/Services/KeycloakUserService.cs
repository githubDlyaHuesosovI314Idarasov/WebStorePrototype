using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using WebStorePrototype.Server.Models;
using WebStorePrototype.Server.Models.DTO_s;

namespace WebStorePrototype.Server.Services
{
    public class KeycloakUserService
    {
        private readonly HttpClient _httpClient;
        private readonly KeycloakAdminTokenService _tokenService;
        private readonly KeycloakConfiguration _keycloakConfiguration;

        public KeycloakUserService(HttpClient httpClient, KeycloakAdminTokenService tokenService, KeycloakConfiguration keycloakConfiguration)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
            _keycloakConfiguration = keycloakConfiguration;
        }

        public async Task<KeycloakUser?> GetUserAsync(String? userId)
        {
            var token = await _tokenService.GetTokenAsync();

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"{_keycloakConfiguration.BaseUrl}/admin/realms/{_keycloakConfiguration.Realm}/users/{userId}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<KeycloakUser>();
        }
    }
}
