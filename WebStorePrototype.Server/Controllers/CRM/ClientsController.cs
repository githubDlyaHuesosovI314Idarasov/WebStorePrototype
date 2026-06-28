using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using WebStorePrototype.Server.Models.API.QueryParams;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.DTO_s.CRM;
using WebStorePrototype.Server.Services.CRM;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebStorePrototype.Server.Controllers.CRM
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsAPIService _clientsService;
        public ClientsController(CRMSettings settings, IMapper mapper)
        {
            _clientsService = RestService.For<IClientsAPIService>(settings.Entrypoint);
        }

        [HttpGet]
        public async Task<IActionResult> GetClientsAsync([Query] GetClientsQueryParams queryParams) {

            var result = await _clientsService.GetClients(queryParams);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostClientsAsync([AliasAs("office_hash_id")] String officeId, [Body] ClientAttributes body)
        {
            var result = await _clientsService.PostClients(officeId, body);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient([AliasAs("id")] Int64 id)
        {
           Client client = await _clientsService.GetClient(id);
           return Ok(client);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchClient([AliasAs("id")] Int64 id)
        {
            var result = await _clientsService.PatchClient(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([AliasAs("id")] Int64 id)
        {
            var result = await _clientsService.DeleteClient(id);
            return Ok(result);
        }

        [HttpPost("{clinent_id}/comments")]
        public async Task<IActionResult> PostClientComments([AliasAs("id")] Int64 id, [Body] Comment body)
        {
            var result = await _clientsService.PostClientComments(id, body);
            return Ok(result);
        }

        [HttpGet("statuses")]
        public async Task<IActionResult> GetClientsStatuses() {
        
            var result = await _clientsService.GetClientsStatuses();
            return Ok(result);
        }
    }
}
