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
    public class OfficesController : ControllerBase
    {
        private readonly IOfficesAPIService _officesService;
        public OfficesController(CRMSettings settings)
        {
            _officesService = RestService.For<IOfficesAPIService>(settings.Entrypoint);
        }

        [HttpGet]
        public async Task<IActionResult> GetOfficesAsync()
        {
            var result = await _officesService.GetOfficesAsync();
            return Ok(result);
        }
    }
}
