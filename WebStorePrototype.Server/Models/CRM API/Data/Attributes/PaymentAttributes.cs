namespace WebStorePrototype.Server.Models.CRM_API.Data.Attributes
{
    public record PaymentAttributes(
        Decimal Amount,
        String Kind,
        String At,
        String Currency,
        String Comment,
        Boolean Planned,
        Int64 PurseId,
        Int64 ParentId,
        Int64 CategoryId,
        Int64 SegmentId
        );
}
