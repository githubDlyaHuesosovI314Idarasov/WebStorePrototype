namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record NewContractorAttributes(
        String Name,
        String? Person,
        List<String> Emails,
        List<String> Phones,
        List<CustomField> CustomFields);
}
