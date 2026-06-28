using Refit;

namespace WebStorePrototype.Server.Models.CRM_API.QueryParams
{
    public record GetAgreementStatusesQueryParams([AliasAs("lost")]Boolean Lost);
}
