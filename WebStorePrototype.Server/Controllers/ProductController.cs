using DAL.EF;
using DAL.Models;
using DAL.Repos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Data;
using System.Data.Common;
using System.Text.Json;
using WebStorePrototype.Server.Features.Base;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Product?>> Get(Guid id)
        {
            var product = await _mediator.Send(new GetByIdQuery<Product>(id));
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _mediator.Send(new GetAllQuery<Product>());
            return Ok(products);

        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            await _mediator.Send(new CreateCommand<Product>(product));
            return Created();
        }

        [HttpPut]
        public async Task<ActionResult<Product>> Update(Product product)
        {
            await _mediator.Send(new UpdateCommand<Product>(product));
            return Ok(product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCommand<Product>(id));
            return NoContent();

        }

        [HttpGet("batch")]
        public async Task<ActionResult<IEnumerable<Product>>> GetBatch([FromQuery] List<Guid> ids)
        {
            if (!ids.Any()) return Ok(Enumerable.Empty<Product>());

            var result = new List<Product>();
            var missing = new List<Guid>();

            foreach (var id in ids)
            {
                var cached = await _redisService.GetAsync($"product:{id}");
                if (cached != null)
                    result.Add(cached);
                else
                    missing.Add(id);
            }

            if (missing.Any())
            {
                var fromDb = (await _productRepo.GetAllAsync())
                    .Where(p => missing.Contains(p.Id))
                    .ToList();

                foreach (var p in fromDb)
                    await _redisService.SetAsync($"product:{p.Id}", p);

                result.AddRange(fromDb);
            }

            return Ok(result);
        }

    }
}
