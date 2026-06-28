using Refit;
using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Models.CRM_API.Response;

namespace WebStorePrototype.Server.Services.CRM
{
    public interface IContractorsAPIService
    {
        [Get("/contractors")]
        public Task<GetListResponse<Contractor>> GetContractors();

        [Post("/contractors")]
        public Task<CreatedResponse> PostContractors([Query] PostContractorsQueryParams queryParams, [Body] NewContractorAttributes newContractorAttributes);
    }
}
