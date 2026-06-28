using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Services.CRM;

namespace WebStorePrototype.Server.Controllers.CRM
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersAPIService _usersService;
        public UsersController(CRMSettings settings)
        {
            _usersService = RestService.For<IUsersAPIService>(settings.Entrypoint);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
            var result = await _usersService.GetUsersAsync();
            return Ok(result);
        }
    }
}
