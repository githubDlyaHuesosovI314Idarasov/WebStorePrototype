namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record Payment(
        Decimal Amount,
        String AmountFormat,
        Int64 ParentId,
        String Kind,
        String At,
        String Currency,
        String Comment,
        Boolean Planned,
        Purse Purse,
        User User,
        PaymentCategory Category,
        Segment Segment
        );
}
