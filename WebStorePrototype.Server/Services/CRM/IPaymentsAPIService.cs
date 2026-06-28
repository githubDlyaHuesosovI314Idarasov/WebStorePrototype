using Refit;
using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Models.CRM_API.RequestBody;
using WebStorePrototype.Server.Models.CRM_API.Response;

namespace WebStorePrototype.Server.Services.CRM
{
    public interface IPaymentsAPIService
    {
        [Get("/payments")]
        public Task<GetListResponse<Payment>> GetPayments();
        
        [Post("/payments")]
        public Task<CreatedResponse> PostPayment([Query] PostPaymentQueryParams queryParams, [Body] PostPaymentRequestBody body);

        [Get("/payments/categories")]
        public Task<GetListResponse<PaymentCategory>> GetPaymentCategories();

        [Get("/payments/purses")]
        public Task<GetListResponse<Purse>> GetPurses();

        [Get("/payments/segments")]
        public Task<GetListResponse<Segment>> GetSegments();


    }
}
