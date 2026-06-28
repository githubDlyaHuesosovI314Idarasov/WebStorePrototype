using Refit;
using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Models.CRM_API.Data;

namespace WebStorePrototype.Server.Services.CRM
{
    public interface IUsersAPIService
    {
        [Get("/users")]
        public Task<GetListResponse<User>> GetUsersAsync();
    }
}
