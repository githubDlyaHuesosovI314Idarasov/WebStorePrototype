using System.Diagnostics.Contracts;

namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record ClientAttributes(
        String Company,
        String? Person,
        String? Email,
        List<String> Phones,
        Boolean Lead,
        Boolean Important,
        String Comment,
        String Birthday,
        String AvatarUrl,
        Int64 ContractorId,
        Int64 SourceId,
        Int64 StatusId,
        Int32 MainResponsibleId,
        Int64 SectionId,
        List<String> TagNames,
        List<CustomField> CustomFields,
        List<Contact> Contacts);
}
