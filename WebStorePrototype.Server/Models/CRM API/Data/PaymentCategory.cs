namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record PaymentCategory(
        Decimal Id,
        Decimal ParentId,
        String Name,
        String Kind);
}
