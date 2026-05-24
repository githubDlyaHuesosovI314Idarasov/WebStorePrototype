using DAL.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStorePrototype.Server.Features.CartProduct.Commands;
using WebStorePrototype.Server.Features.CartProduct.Queries;

namespace WebStorePrototype.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartProduct>>> GetCartProducts([FromQuery] String? userId)
        {
            return Ok(await _mediator.Send(new GetCartProductQuery(userId)));
        }

        [HttpPost("{productId:guid}")]
        public async Task<IActionResult> AddCartProduct(Guid productId, [FromQuery] String? userId)
        {
            await _mediator.Send(new AddCartProductCommand(productId, userId));
            return NoContent();
        }

        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> RemoveCartProduct(Guid productId, [FromQuery] String? userId)
        {
            await _mediator.Send(new RemoveCartProductCommand(productId, userId));
            return NoContent();
        }

        [HttpPost("merge")]
        public async Task<IActionResult> Merge([FromQuery] String userId)
        {
            if (String.IsNullOrWhiteSpace(userId)) { return BadRequest(); }
            await _mediator.Send(new MergeCookieCartProductsCommand(userId));
            return NoContent();
        }

    }
}
