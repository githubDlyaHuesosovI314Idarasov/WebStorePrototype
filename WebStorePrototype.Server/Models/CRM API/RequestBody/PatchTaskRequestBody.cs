using WebStorePrototype.Server.Models.CRM_API.Data;

namespace WebStorePrototype.Server.Models.CRM_API.RequestBody
{
    public record PatchTaskRequestBody(
        String Title,
        String Comment,
        String DeadlineAt,
        Decimal StatusId,
        Int64 UserId,
        Int64 CategoryId,
        List<CustomField> CustomFields);
}
