using WebStorePrototype.Server.Models.CRM_API.Data.ShortData;

namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record Agreement(
        Int64 Id,
        String Title,
        String CreatedAt,
        String UpdatedAt,
        String MarketplaceCreatedAt,
        Decimal Total,
        String Comment,
        Boolean Important,
        Decimal Discount,
        DiscountType DiscountKind,
        AgreementStage Stage,
        AgreementSource Source,
        List<DeliveryShort> Deliveries,
        AgreementResult Result,
        Boolean ProductsTotalAsTotal,
        Client Client,
        User MainResponsible,
        List<Job> Jobs,
        List<CustomField> CustomFields
        );

    public enum DiscountType { PercentDiscount, AbsoluteDiscount };
    public enum AgreementResult { Archived, Successful, Failed };

    public record AgreementStage(String Name);
    public record AgreementSource(Decimal Id, String Name);



}
