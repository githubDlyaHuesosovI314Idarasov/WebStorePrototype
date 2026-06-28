namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record ProductCategory(
        Decimal Id,
        Decimal ParentId,
        String Name,
        String FullPath,
        List<Object> Children);
}
