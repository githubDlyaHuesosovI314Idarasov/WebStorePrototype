using Refit;
using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Models.CRM_API.Response;

namespace WebStorePrototype.Server.Services.CRM
{
    public interface IDeliveriesAPIService
    {
        [Get("/deliveries")]
        public Task<GetListResponse<Delivery>> GetDeliveriesAsync([Query] GetDeliveriesQueryParams queryParams);
    }
}
