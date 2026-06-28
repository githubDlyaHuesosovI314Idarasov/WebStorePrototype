namespace WebStorePrototype.Server.Models.CRM_API.Data.Attributes
{
    public record ContractorAttributes(String Name, String? Person, String? Email, String? Phone, String? Comment, List<CustomField> CustomFields);
}
