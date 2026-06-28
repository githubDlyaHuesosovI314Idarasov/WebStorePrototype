using Refit;

namespace WebStorePrototype.Server.Models.API.QueryParams
{
    public class PostClientsQueryParams
    {
        [AliasAs("office_hash_id")]
        public String? OfficeId { get; set; }
    }
}
