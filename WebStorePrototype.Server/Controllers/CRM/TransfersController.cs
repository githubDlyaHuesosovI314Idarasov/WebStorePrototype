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
    public class TransfersController : ControllerBase
    {
        private readonly ITransfersAPIService _transfersService;
        public TransfersController(CRMSettings settings)
        {
            _transfersService = RestService.For<ITransfersAPIService>(settings.Entrypoint);
        }

        public async Task<IActionResult> PostTransfers([Body] TransferAttributes transferAttributes)
        {
            var result = await _transfersService.PostTransfers(transferAttributes);
            return Ok(result);
        }
    }
}
