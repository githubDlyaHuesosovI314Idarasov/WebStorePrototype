using DAL.Models;

namespace WebStorePrototype.Server.Models.CRM_API.Data.Attributes
{
    public record JobAttribute(
        Product ProductAttributes,
        Int64 Id,
        Boolean _Destroy,
        String Title,
        Decimal Amount,
        Decimal Discount,
        String Cost,
        String Price,
        DiscountType DiscountKind,
        List<CustomField> CustomFields
        );
}
