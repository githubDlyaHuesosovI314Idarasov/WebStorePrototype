namespace WebStorePrototype.Server.Models.CRM_API.Data.Attributes
{
    public record ProductAttributes(
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
        Decimal CategoryId,
        String VatGroupTitle,
        Boolean Irrelevant,
        List<CustomField> CustomFields,
        List<CustomPrice> CustomPrices);
}
