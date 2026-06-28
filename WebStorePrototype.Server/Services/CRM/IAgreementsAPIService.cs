using Refit;
using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Models.CRM_API.RequestBody;
using WebStorePrototype.Server.Models.CRM_API.Response;

namespace WebStorePrototype.Server.Services.CRM
{
    public interface IAgreementsAPIService
    {
        [Get("/agreements")]
        public Task<GetListResponse<Agreement>> GetAgreements([Query] GetAgreementsQueryParams queryParams);

        [Post("/agreements")]
        public Task<CreatedResponse> PostAgreements([Query] PostAgreementsQueryParams queryParams, [Body] AgreementAttributes agreementAttributes);

        [Get("/agreements/{id}")]
        public Task<Agreement> GetAgreement(Int64 id);

        [Patch("/agreements/{id}")]
        public Task<CreatedResponse> PatchAgreement(Int64 id, [Body] PatchAgreementRequestBody body);

        [Delete("/agreements/{id}")]
        public Task<DeletedResponse> DeleteAgreement(Int64 id);

        [Post("/agreemennts/{agreement_id}/comments")]
        public Task<CreatedResponse> PostComment([AliasAs("agreement_id")]Int64 id, [Body] CommentBody body);

        [Post("/agreements/{agreement_id}/deliveries")]
        public Task<CreatedResponse> PostDelivery([AliasAs("agreement_id")] Int64 id, [Body] DeliveryBody body);

        [Get("/agreements/funnels")]
        public Task<GetListResponse<Funnel>> GetFunnels();

        [Get("/agreements/stages")]
        public Task<GetListResponse<Stage>> GetStages();

        [Get("/agreements/statuses")]
        public Task<GetListResponse<AgreementStatus>> GetStatuses([Query] GetAgreementStatusesQueryParams queryParams);

    }
}
