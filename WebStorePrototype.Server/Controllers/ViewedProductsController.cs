using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStorePrototype.Server.Features.Base;
using WebStorePrototype.Server.Features.FavoriteProduct.Commands;
using WebStorePrototype.Server.Features.ViewedProduct.Commands;
using WebStorePrototype.Server.Features.ViewedProduct.Queries;
using WebStorePrototype.Server.Models.DTO_s;
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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<ViewedProductDTO>>> GetViewedProductsAsync([FromQuery] Guid id) {
            return Ok(await _mediator.Send(new GetViewedProductsQuery(id)));
        }

        [HttpPost("{productId:guid}")]
        public async Task<IActionResult> AddViewedProductProducts(Guid productId, [FromQuery] String? userId) 
        {
            await _mediator.Send(new AddViewedProductCommand(productId, userId));
            return Created();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveFavoriteProduct(Guid id)
        {
            await _mediator.Send(new RemoveViewedProductCommand(id));
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFavoriteProduct(FavoriteProduct product)
        {
            return Ok( await _mediator.Send(new UpdateFavoriteProductCommand(product)));
        }

        [HttpGet("batch")]
        public async Task<ActionResult<IEnumerable<ViewedProduct>>> Batch(List<Guid> ids)
        {
            var viewedProducts = await _mediator.Send(new GetBatchQuery<ViewedProduct>(ids));
            return Ok(viewedProducts);
        }



    }
}
