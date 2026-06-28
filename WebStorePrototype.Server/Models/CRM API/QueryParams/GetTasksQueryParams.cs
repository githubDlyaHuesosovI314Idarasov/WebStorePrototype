using Refit;

namespace WebStorePrototype.Server.Models.CRM_API.QueryParams
{
    public class GetTasksQueryParams
    {
        [AliasAs("q[created_at_lteq]")]
        public String CreatedAtLteq { get; set; } = null!;

        [AliasAs("q[created_at_gteq]")]
        public String CreatedAtGteq { get; set; } = null!;

        [AliasAs("q[status_id_eq]")]
        public String StatusIdEq { get; set; } = null!;

        [AliasAs("page")]
        public Int64 Page { get; set; } = 1;

    }
}
