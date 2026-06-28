using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Services.CRM;

namespace WebStorePrototype.Server.Controllers.CRM
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDeliveriesAPIService _service;
        public DeliveriesController(CRMSettings settings)
        {
            _service = RestService.For<IDeliveriesAPIService>(settings.Entrypoint);
        }

        public async Task<IActionResult> GetDeliveriesAsync([Query] GetDeliveriesQueryParams queryParams)
        {
            var response = await _service.GetDeliveriesAsync(queryParams);
            return Ok(response);
        }

    }

}
