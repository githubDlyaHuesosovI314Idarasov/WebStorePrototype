namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record Contact(String Fullname, String Email, List<String> Phones, List<CustomField> CustomFields);
}
