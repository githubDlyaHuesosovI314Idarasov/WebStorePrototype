using Refit;
using System.Runtime.CompilerServices;
using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.Data.Attributes;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Models.CRM_API.Response;

namespace WebStorePrototype.Server.Services.CRM
{
    public interface IProductsAPIService
    {
        [Get("/products")]
        public Task<GetListResponse<Product>> GetProducts([Query] GetProductsQueryParams queryParams);

        [Post("/products")]
        public Task<CreatedResponse> PostProduct([Query] PostProductQueryParams queryParams,[Body] ProductAttributes body);

        [Get("/products/{id}")]
        public Task<Product> GetProduct(Int64 id);

        [Patch("/products/{id}")]
        public Task<CreatedResponse> PatchProduct(Int64 id, [Body] ProductAttributes body);

        [Delete("/products/{id}")]
        public Task<DeletedResponse> DeleteProduct(Int64 id);

        [Get("/products/sku/{product_sku}")]
        public Task<Product> GetProductBySKU([AliasAs("product_sku")] String ProductSku);

        [Patch("/products/sku/{product_sku}")]
        public Task<CreatedResponse> PatchProductBySKU([AliasAs("product_sku")] String ProductSku, [Body] ProductAttributes body);

        [Delete("/product/sku/{product_sku}")]
        public Task<DeletedResponse> DeleteProductBySKU([AliasAs("product_sku")] String ProductSku);

        [Get("/products/categories")]
        public Task<GetListResponse<ProductCategory>> GetProductCategories();

        [Post("/products/categories")]
        public Task<CreatedResponse> PostProductCategory([Query] PostProductCategoriesQueryParams queryParams, [Body] ProductCategoryAttributes body);


    }
}
