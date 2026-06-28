namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record Contractor(
        Decimal Id, 
        String Name, 
        String Person, 
        List<String> Emails,
        List<String> Phones);
}
