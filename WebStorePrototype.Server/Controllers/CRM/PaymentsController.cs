using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Runtime.CompilerServices;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Models.CRM_API.RequestBody;
using WebStorePrototype.Server.Services.CRM;

namespace WebStorePrototype.Server.Controllers.CRM
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentsAPIService _paymentService;
        public PaymentsController(CRMSettings settings)
        {
            _paymentService = RestService.For<IPaymentsAPIService>(settings.Entrypoint);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var result = await _paymentService.GetPayments();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostPayment([Query] PostPaymentQueryParams queryParams, [Body] PostPaymentRequestBody body)
        {
            var result = await _paymentService.PostPayment(queryParams, body);
            return Ok(result);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetPaymentCategories()
        {
            var result = await _paymentService.GetPaymentCategories();
            return Ok(result);
        }

        [HttpGet("purses")]
        public async Task<IActionResult> GetPurses()
        {
            var result = await _paymentService.GetPurses();
            return Ok(result);
        }

        [HttpGet("segments")]
        public async Task<IActionResult> GetSegments()
        {
            var result = await _paymentService.GetSegments();
            return Ok(result);
        }

    }
}
