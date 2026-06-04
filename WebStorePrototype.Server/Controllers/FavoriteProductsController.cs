using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Net.WebSockets;
using System.Text.Json;
using WebStorePrototype.Server.Features.FavoriteProduct.Commands;
using WebStorePrototype.Server.Features.FavoriteProduct.Queries;
using WebStorePrototype.Server.Models.DTO_s;
using WebStorePrototype.Server.Services;
using WebStorePrototype.Server.Services.Base;

namespace WebStorePrototype.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteProductsController : Controller
    {
        private readonly IMediator _mediator;

        public FavoriteProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<FavoriteProductDTO>>> GetFavoriteProduct([FromQuery] Guid id)
        {
            return Ok(await _mediator.Send(new GetFavoriteProductQuery(id)));
        }

        [HttpPost("{productId:guid}")]
        public async Task<IActionResult> AddFavoriteProduct(Guid productId, [FromQuery] String? userId)
        {
            await _mediator.Send(new AddFavoriteProductCommand(productId, userId));
            return Created();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveFavoriteProduct([FromQuery]Guid id)
        {
            await _mediator.Send(new RemoveFavoriteProductCommand(id));
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFavoriteProduct(FavoriteProduct product)
        {
            return Ok(await _mediator.Send(new UpdateFavoriteProductCommand(product)));
        }
    }

}

