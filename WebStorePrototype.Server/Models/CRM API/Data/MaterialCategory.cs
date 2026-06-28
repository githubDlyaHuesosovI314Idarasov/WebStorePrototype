namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record MaterialCategory(
        Decimal Id,
        Decimal ParentId,
        String Name,
        String Fullname,
        List<Object> Children
        );
}
