namespace WebStorePrototype.Server.Models.Base
{
    public class RequestBody
    {
        public String ApiKey { get; set; } = null!;
        public String ModelName { get; set; } = null!;
        public String CalledMethod { get; set; } = null!;
    }
}
