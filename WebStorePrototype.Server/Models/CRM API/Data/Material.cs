using WebStorePrototype.Server.Models.CRM_API.Data.Attributes;

namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record Material(
        Decimal Id,
        String SKU,
        String Title,
        String Unit,
        Decimal Price,
        Decimal PriceAmount,
        String Currnency,
        String AssetUrl,
        Decimal CategoryId,
        Category Category,
        List<CustomField> CustomFields,
        Decimal Available,
        List<StockRestsAttributes> StockRests,
        List<Attachment> Attachments
        );
}
