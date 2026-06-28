namespace WebStorePrototype.Server.Models.Base
{
    public record class BaseResponse(Boolean Success, List<String> Errors, List<String> Warnings);
}
