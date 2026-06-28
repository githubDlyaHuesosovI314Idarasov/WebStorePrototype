using WebStorePrototype.Server.Models.CRM_API.Data.Attributes;

namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record AgreementAttributes(
        String Title,
        String Total,
        String Currency,
        String Comment,
        Boolean Important,
        Decimal Discount,
        DiscountType DiscountKind,
        Decimal StageId,
        Decimal SourceId,
        Decimal FunnelId,
        Int64 MainResponsibleId,
        Int64 ClientId,
        Boolean ProductsTotalAsTotal,
        String DeadlineAt,
        AgreementResult Result,
        Int64 ArchiveStatusId,
        ClientAttributes ClientAttributes,
        ContractorAttributes ContractorAttributes,
        List<JobAttribute> JobsAttributes,
        List<CustomField> CustomFields
        );
}
