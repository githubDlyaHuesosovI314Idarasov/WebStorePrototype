namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record Client(
        Int32 Id,
        String Compamy,
        String Person,
        String Email,
        List<String> Phones,
        Boolean Lead,
        Boolean Important,
        String Comment,
        Int32 ContractorId,
        Source Source,
        User MainResponsible,
        List<String> TagNames,
        List<CustomField> CustomFields,
        List<Contact> Contacts,
        ProfileShort Profile,
        ClientShort ClientShort
        
        );
}
