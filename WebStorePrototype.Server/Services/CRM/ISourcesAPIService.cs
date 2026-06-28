using Refit;
using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Models.CRM_API.Data;

namespace WebStorePrototype.Server.Services.CRM
{
    public interface ISourcesAPIService
    {
        [Get("/sources")]
        public Task<GetListResponse<Source>> GetSourcesAsync();
    }
}
