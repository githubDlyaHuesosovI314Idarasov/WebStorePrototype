namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record Product(
        Decimal Id,
        String SKU,
        String Title,
        String? Unit,
        Decimal Price,
        Decimal PriceAmount,
        String? Currency,
        String? AssetUrl,
        Decimal CategoryId,
        Category Category,
        List<CustomField> CustomFields,
        List<Attachment> Attachments
        );


}
