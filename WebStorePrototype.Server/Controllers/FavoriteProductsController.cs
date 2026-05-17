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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteProductDTO>>> GetProducts(
        [FromQuery] String? userId)
        {
            return Ok(await _mediator.Send(new GetFavoriteProducyQuery(userId)));
        }

        [HttpPost("{productId:guid}")]
        public async Task<IActionResult> AddProduct(Guid productId, [FromQuery] String? userId)
        {
            await _mediator.Send(new AddFavoriteProductCommand(productId, userId));
            return NoContent();
        }

        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> RemoveProduct(Guid productId, [FromQuery] String? userId)
        {
            await _mediator.Send(new RemoveFavoriteProductCommand(productId, userId));
            return NoContent();
        }

        [HttpPost("merge")]
        public async Task<IActionResult> MergeCookie([FromQuery] String userId)
        {
            if(String.IsNullOrWhiteSpace(userId)) { return BadRequest(); }
            await _mediator.Send(new MergeCookieFavoritesCommand(userId));
            return NoContent();
        }
    }

}

