using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Runtime.CompilerServices;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.Data.Attributes;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Services.CRM;

namespace WebStorePrototype.Server.Controllers.CRM
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialsAPIService _materialsService;
        public MaterialsController(CRMSettings settings)
        {
            _materialsService = RestService.For<IMaterialsAPIService>(settings.Entrypoint);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaterials([Query] GetMaterialsQueryParams queryParams)
        {
            var result = await _materialsService.GetMaterials(queryParams);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostMaterial([Query] PostMaterialsQueryParams queryParams, [Body] MaterialAttributes body)
        {
            var result = await _materialsService.PostMaterials(queryParams, body);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<Material> GetMaterial(Int64 id)
        {
            Material material = await _materialsService.GetMaterial(id);
            return material;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMaterial(Int64 id, [Body] MaterialUpdateAttributes body)
        {
            var result = await _materialsService.PatchMaterial(id, body);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(Int64 id)
        {
            var result = await _materialsService.DeleteMaterial(id);
            return Ok(result);
        }

        [HttpPost("{id}/offers")]
        public async Task<IActionResult> PostMaterialModification(Int64 id, [Body] MaterialUpdateAttributes body)
        {
            var result = await _materialsService.PostMaterialModification(id, body);
            return Ok(result);
        }

        [HttpGet("sku/{SKU}")]
        public async Task<Material> GetMaterialBySKU([AliasAs("material_sku")] String SKU)
        {
            Material material = await GetMaterialBySKU(SKU);
            return material;
        }

        [HttpPatch("sku/{SKU}")]
        public async Task<IActionResult> PatchMaterialBySKU([AliasAs("material_sku")] String SKU, [Body] MaterialUpdateAttributes body)
        {
            var result = await _materialsService.PatchMaterialBySKU(SKU, body);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMaterialBySKU([AliasAs("material_sku")] String SKU) 
        {
            var result = await _materialsService.DeleteMaterialBySKU(SKU);
            return Ok(result);
        }

        [HttpPost("sku/{SKU}/offers")]
        public async Task<IActionResult> PostMaterialModificationBySKU([AliasAs("material_sku")] String SKU, [Body] MaterialUpdateAttributes body)
        {
            var result = await _materialsService.PostMaterialModificationBySKU(SKU, body);
            return Ok(result);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetMaterialCategories()
        {
            var result = await _materialsService.GetMaterialCategories();
            return Ok(result);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> PostMaterialCategories([Query] PostMaterialsCategoryQueryParams queryParams, [Body] ProductCategoryAttributes body)
        {
            var result = await _materialsService.PostMaterialsCategory(queryParams, body);
            return Ok(result);
        }
    }
}
