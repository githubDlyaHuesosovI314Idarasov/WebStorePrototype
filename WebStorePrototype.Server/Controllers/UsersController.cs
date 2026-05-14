using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly KeycloakUserService _keycloakUserService;

        public UsersController(KeycloakUserService keycloakUserService)
        {
            _keycloakUserService = keycloakUserService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser() { 
        
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");

            if (String.IsNullOrEmpty(userId)) 
            {
                return Unauthorized("User id not found in token");
            }
            
            var user = await _keycloakUserService.GetUserAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
    }
}
