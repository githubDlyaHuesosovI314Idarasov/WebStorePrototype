namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record Job(
        Decimal Id,
        String Title,
        String UpdatedAt,
        Decimal Amount,
        DiscountType DiscountType,
        String Cost,
        String Price,
        String Total,
        List<CustomField> CustomFields
        );
}
