namespace WebStorePrototype.Server.Models.DTO_s.CRM
{
    public sealed record ClientDTO(
        String Company,
        String Person,
        String Email,
        List<String> Phones,
        List<ContactDTO> Contacts
        );
}
