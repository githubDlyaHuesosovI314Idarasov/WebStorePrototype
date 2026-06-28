using Refit;
using WebStorePrototype.Server.Models.API.QueryParams;
using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.Response;
using WebStorePrototype.Server.Models.DTO_s.CRM;

namespace WebStorePrototype.Server.Services.CRM
{
    public interface IClientsAPIService
    {
        [Get("/clients")]
        public Task<GetListResponse<List<Client>>> GetClients([Query] GetClientsQueryParams queryParams);

        [Post("/clients")]
        public Task<CreatedResponse> PostClients([AliasAs("office_hash_id")] String officeId, [Body] ClientAttributes body);

        [Get("/clients/{id}")]
        public Task<Client> GetClient([AliasAs("id")] Int64 id);

        [Patch("/clients/{id}")]
        public Task<CreatedResponse> PatchClient([AliasAs("id")] Int64 id);

        [Delete("/clients/{id}")]
        public Task<DeletedResponse> DeleteClient([AliasAs("id")] Int64 id);

        [Post("/clients/{client_id}/comments")]
        public Task<CreatedResponse> PostClientComments([AliasAs("client_id")] Int64 id, [Body] Comment comment);

        [Get("/clients/statuses")]
        public Task<GetListResponse<ClientStatus>> GetClientsStatuses();
    } 

}
