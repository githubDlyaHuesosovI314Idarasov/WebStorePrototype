using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Net;
using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Models.CRM_API.RequestBody;
using WebStorePrototype.Server.Services.CRM;

namespace WebStorePrototype.Server.Controllers.CRM
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgreementsController : ControllerBase
    {
        private readonly IAgreementsAPIService _agreementsService;
        public AgreementsController(CRMSettings settings)
        {
            _agreementsService = RestService.For<IAgreementsAPIService>(settings.Entrypoint);
        }

        [HttpGet]
        public async Task<IActionResult> GetAgreementsAsync([Query] GetAgreementsQueryParams queryParams)
        {
            var result = await _agreementsService.GetAgreements(queryParams);
            return Ok(result);  
        }

        [HttpPost]
        public async Task<IActionResult> PostAgreementsAsync([Query] PostAgreementsQueryParams queryParams, [Body] AgreementAttributes agreementAttributes)
        {
            var result = await _agreementsService.PostAgreements(queryParams, agreementAttributes);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgreementAsync(Int64 id)
        {
            var result = await _agreementsService.GetAgreement(id); 
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAgreement(Int64 id, [Body] PatchAgreementRequestBody body)
        {
            var result = await _agreementsService.PatchAgreement(id, body);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgreement(Int64 id)
        {
            var result = await _agreementsService.DeleteAgreement(id);
            return Ok(result);
        }

        [HttpPost("/{agreeement_id}/comments")]
        public async Task<IActionResult> PostComment([AliasAs("agreement_id")] Int64 id, [Body] CommentBody body)
        {
            var result = await _agreementsService.PostComment(id, body);
            return Ok(result);

        }

        [HttpPost("/{agreeement_id}/delivery")]
        public async Task<IActionResult> PostDelivery([AliasAs("agreement_id")] Int64 id, [Body] DeliveryBody body)
        {
            var result = await _agreementsService.PostDelivery(id, body);
            return Ok(result);
        }

        [HttpGet("funnels")]
        public async Task<IActionResult> GetFunnels()
        {
            var result = await _agreementsService.GetFunnels();
            return Ok(result);
        }

        [HttpGet("statges")]
        public async Task<IActionResult> GetStages() 
        {
            var result = await _agreementsService.GetStages();
            return Ok(result);
        }

        [HttpGet("statuses")]
        public async Task<GetListResponse<AgreementStatus>> GetStatuses([Query] GetAgreementStatusesQueryParams queryParams)
        {
            var result = await _agreementsService.GetStatuses(queryParams);
            return result;
        }

    }
}
