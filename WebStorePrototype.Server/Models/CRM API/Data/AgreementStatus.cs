namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record AgreementStatus(
        Decimal Id,
        String Name,
        String Kind,
        String Description,
        String Color,
        Funnel Funnel
        );
}
