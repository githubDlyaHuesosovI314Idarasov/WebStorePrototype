using Refit;

namespace WebStorePrototype.Server.Models.CRM_API.QueryParams
{
    public class PostAgreementsQueryParams {

        [AliasAs("office_hash_id")]
        public String OfficeId { get; set; } = null!;
    };
}
