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
    public class SourcesController : ControllerBase
    {
        private readonly ISourcesAPIService _sourcesServices;
        public SourcesController(CRMSettings settings)
        {
            _sourcesServices = RestService.For<ISourcesAPIService>(settings.Entrypoint);
        }

        [HttpGet]
        public async Task<IActionResult> GetSources()
        {
            var result = await _sourcesServices.GetSourcesAsync();
            return Ok(result);
        }
    }
}
