using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStorePrototype.Server.Features.ViewedProduct.Commands;
using WebStorePrototype.Server.Features.ViewedProduct.Queries;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewedProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewedProductsController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewedProduct>>> GetViewedProductsAsync([FromQuery] String? userId) {
            return Ok(await _mediator.Send(new GetViewedProductsQuery(userId)));
        }

        [HttpPost("{productId:guid}")]
        public async Task<IActionResult> Track(Guid productId, [FromQuery] String? userId) {

            await _mediator.Send(new TrackViewedProductCommand(productId, userId));
            return NoContent();
        }

    }
}
