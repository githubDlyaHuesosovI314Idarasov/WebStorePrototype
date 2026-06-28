using System.ComponentModel;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.Data.Attributes;

namespace WebStorePrototype.Server.Models.CRM_API.RequestBody
{
    public record PatchAgreementRequestBody(
        Decimal StageId,
        Decimal FunnelId,
        Int64 MainResponsibleId,
        Decimal ClientId,
        String Comment,
        AgreementResult AgreementResult,
        Int64 ArchiveStatusId,
        Decimal Total,
        Boolean ProductsTotalAsTotal,
        List<JobAttribute> JobAttributes,
        List<CustomField> CustomFields
        );
}
