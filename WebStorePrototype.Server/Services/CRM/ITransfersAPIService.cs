using Refit;
using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.Response;

namespace WebStorePrototype.Server.Services.CRM
{
    public interface ITransfersAPIService
    {

        [Post("/transfers")]
        public Task<CreatedResponse> PostTransfers([Body] TransferAttributes transferAttributes);
    }
}
