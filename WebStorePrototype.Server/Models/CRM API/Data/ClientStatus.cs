namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record ClientStatus(
        Int32 id, 
        String name,
        String kind,
        String description,
        String color,
        Section section);
}
