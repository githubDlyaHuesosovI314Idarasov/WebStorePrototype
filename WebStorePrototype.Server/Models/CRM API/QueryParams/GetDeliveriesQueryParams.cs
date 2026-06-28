using Refit;
using WebStorePrototype.Server.Models.CRM_API.Data;

namespace WebStorePrototype.Server.Models.CRM_API.QueryParams
{
    public class GetDeliveriesQueryParams
    {
        [AliasAs("ttn_eq")]
        public String TtnEq { get; set; } = null!;

        [AliasAs("delivery_serivce_type_eq")]
        public DeliveryServiceType DeliveryServiceType { get; set; }
    }
}
