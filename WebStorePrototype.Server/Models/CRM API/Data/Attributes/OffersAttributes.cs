namespace WebStorePrototype.Server.Models.CRM_API.Data.Attributes
{
    public record OffersAttributes(
        String SKU,
        String Title,
        String Unit,
        Decimal Price,
        Decimal Cost,
        String Currency,
        String CostCurrency,
        Decimal Weight,
        Decimal Volume,
        String AssetUrl,
        String LinkUrl,
        Boolean Irrelevant,
        Decimal Available,
        List<StockRestsAttributes> StockRestsAttributes,
        List<CustomField> CustomFields,
        List<CustomPrice> CustomPrices
        );
}
