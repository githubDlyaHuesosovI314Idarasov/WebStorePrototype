namespace WebStorePrototype.Server.Models.CRM_API.Data.Attributes
{
    public record MaterialAttributes(
        String SKU,
        String Title,
        String Unit,
        Decimal Price,
        Decimal Cost,
        String Currency,
        String CostCurency,
        Decimal Weight,
        Decimal Volume, 
        String AssetUrl,
        String LinkUrl,
        Decimal CategoryId,
        String VatGroupTitle,
        Boolean Irrelevant,
        Decimal Available,
        List<StockRestsAttributes> StockRestsAttributes,
        List<CustomField> CustomFields,
        List<CustomPrice> NamedCustomPrice,
        List<OffersAttributes> OffersAttributes
            );
}
