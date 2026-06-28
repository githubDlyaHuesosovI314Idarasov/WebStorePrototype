using Refit;

namespace WebStorePrototype.Server.Models.CRM_API.QueryParams
{
    public class PostProductQueryParams()
    {
        [AliasAs("q[office_hash_id]")]
        public String OfficeId { get; set; } = null!;
    }
}
