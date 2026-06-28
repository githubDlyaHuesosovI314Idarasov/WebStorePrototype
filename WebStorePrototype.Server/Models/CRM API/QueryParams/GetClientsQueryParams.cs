using Refit;

namespace WebStorePrototype.Server.Models.API.QueryParams
{
    public class GetClientsQueryParams{
        [AliasAs("q[source_id_eq]")]    
        public String? SourceId { get; set; }

        [AliasAs("q[registered_at_lteq]")]
        public String? RegistredAtLte { get; set; } 
        
        [AliasAs("q[registered_at_gteq]")]
        public String? RegisteredAtGte { get; set; }
        
        [AliasAs("q[trigram_idx_cont]")]
        public String? Search { get; set; }
        
        [AliasAs("page")]
        public Int32 Page { get; set; }
    };
}
