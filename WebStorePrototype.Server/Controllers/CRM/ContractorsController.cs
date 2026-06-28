using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Runtime.CompilerServices;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Models.CRM_API.Response;
using WebStorePrototype.Server.Models.DTO_s.CRM;
using WebStorePrototype.Server.Services.CRM;

namespace WebStorePrototype.Server.Controllers.CRM
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorsController : ControllerBase
    {
        private readonly IContractorsAPIService _contractorsService;

        public ContractorsController(CRMSettings settings)
        {
            _contractorsService = RestService.For<IContractorsAPIService>(settings.Entrypoint);
        }

        [HttpGet]
        public async Task<IActionResult> GetContractors()
        {
            var result = await _contractorsService.GetContractors();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostContractors([Query] PostContractorsQueryParams queryParams, [Body] NewContractorAttributes newContractorAttributes)
        {
           var result = await _contractorsService.PostContractors(queryParams, newContractorAttributes);
           return Ok(result);
        }
    }
}
