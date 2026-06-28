using Refit;

namespace WebStorePrototype.Server.Models.CRM_API.QueryParams
{
    public class GetProductsQueryParams
    {
        [AliasAs("q[category_id_eq]")]
        public String CategoryIdEq { get; set; } = null!;

        [AliasAs("page")]
        public Int64 Page { get; set; } = 1;
    }
}
