using Refit;

namespace WebStorePrototype.Server.Models.CRM_API.QueryParams
{
    public class GetAgreementsQueryParams
    {
        [AliasAs("q[ordered_at_lteq]")]
        public String? OrderedAtLteq { get; set; }

        [AliasAs("q[ordered_at_gteq]")]
        public String? OrderedAtGteq { get; set; }
        
        [AliasAs("q[source_id_eq]")]
        public String? OrderedIdEq { get; set; }
        
        [AliasAs("q[client_id_eq]")]
        public String? ClientIdEq { get; set; }

        [AliasAs("q[result_eq]")]
        public AgreementResult ResutlEq { get; set; }

        [AliasAs("page")]
        public Int64 Page { get; set; } = 1;
    }

    public enum AgreementResult
    {
        Archived,
        Successful,
        Failed
    }
}
