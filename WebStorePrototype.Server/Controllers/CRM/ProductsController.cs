using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Runtime.InteropServices;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.Data.Attributes;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Services.CRM;

namespace WebStorePrototype.Server.Controllers.CRM
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsAPIService _productService;
        public ProductsController(CRMSettings settings)
        {
            _productService = RestService.For<IProductsAPIService>(settings.Entrypoint);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([Query] GetProductsQueryParams queryParams) {
            
            var result = await _productService.GetProducts(queryParams);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([Query] PostProductQueryParams queryParams, [Body] ProductAttributes body)
        {
            var result = await _productService.PostProduct(queryParams, body);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProduct(Int64 id)
        {
            Product product  = await _productService.GetProduct(id);
            return product;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduct(Int64 id, [Body] ProductAttributes body)
        {
            var result = await _productService.PatchProduct(id, body);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Int64 id)
        {
            var result = await _productService.DeleteProduct(id);
            return Ok(result);
        }

        [HttpGet("sku/{product_sku}")]
        public async Task<Product> GetProductBySKU([AliasAs("product_sku")] String ProductSKU)
        {
            Product product = await _productService.GetProductBySKU(ProductSKU);
            return product;
        }

        [HttpPatch("sku/{product_sku}")]
        public async Task<IActionResult> PatchProductBySKU([AliasAs("product_sku")] String ProductSKU, [Body] ProductAttributes body)
        {
            var result = await _productService.PatchProductBySKU(ProductSKU, body);
            return Ok(result);
        }

        [HttpDelete("sku/{product_sku}")]
        public async Task<IActionResult> DeleteProductBySKU([AliasAs("product_sku")] String ProductSKU)
        {
            var result = await _productService.DeleteProductBySKU(ProductSKU);
            return Ok(result);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetProductCategories()
        {
            var result = await _productService.GetProductCategories();
            return Ok(result);
        }

        public async Task<IActionResult> PostProductCategory([Query] PostProductCategoriesQueryParams queryParams, [Body] ProductCategoryAttributes body)
        {
            var result = await _productService.PostProductCategory(queryParams, body);
            return Ok(result);
        }
    }
}
