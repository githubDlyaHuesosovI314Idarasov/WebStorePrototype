using Refit;
using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.Data.Attributes;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Models.CRM_API.Response;

namespace WebStorePrototype.Server.Services.CRM
{
    public interface IMaterialsAPIService
    {
        [Get("/materials")]
        public Task<GetListResponse<Material>> GetMaterials([Query] GetMaterialsQueryParams queryParams);

        [Post("/materials")]
        public Task<CreatedResponse> PostMaterials([Query] PostMaterialsQueryParams queryParams, [Body] MaterialAttributes body);

        [Get("/materials/{id}")]
        public Task<Material> GetMaterial(Int64 id);

        [Patch("/materials/{id}")]
        public Task<CreatedResponse> PatchMaterial(Int64 id, [Body] MaterialUpdateAttributes body);

        [Delete("/materials/{id}")]
        public Task<DeletedResponse> DeleteMaterial(Int64 id);

        [Post("/materials/{id}/offers")]
        public Task<CreatedResponse> PostMaterialModification(Int64 id, [Body] MaterialUpdateAttributes body);

        [Get("/materials/sku/{material_sku}")]
        public Task<Material> GetMaterialBySKU([AliasAs("material_sku")]String SKU);

        [Patch("/materials/sku/{material_sku}")]
        public Task<CreatedResponse> PatchMaterialBySKU([AliasAs("material_sku")] String SKU, [Body] MaterialUpdateAttributes body);

        [Delete("/materials/sku/{material_sku}")]
        public Task<DeletedResponse> DeleteMaterialBySKU([AliasAs("material_sku")] String SKU);

        [Post("/materials/sku/{material_sku}/offers")]
        public Task<CreatedResponse> PostMaterialModificationBySKU([AliasAs("material_sku")] String SKU, [Body] MaterialUpdateAttributes body);

        [Get("/materials/categories")]
        public Task<GetListResponse<MaterialCategory>> GetMaterialCategories();

        [Post("/materials/categories")]
        public Task<CreatedResponse> PostMaterialsCategory([Query] PostMaterialsCategoryQueryParams queryParams, [Body] ProductCategoryAttributes body);


    }
}
