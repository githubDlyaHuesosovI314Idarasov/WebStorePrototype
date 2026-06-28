namespace WebStorePrototype.Server.Models.CRM_API.Data
{
    public record TransferAttributes(
        TransferType Type,
        Decimal Amount,
        Decimal OfficeId,
        String OfficeHashId,
        String OfficeName,
        String MaterialSKU,
        Decimal MaterialId,
        Decimal Number,
        String Comment
        );

    public enum TransferType
    {
        Debit,
        Transfer,
        UploadRevert
    }
}
