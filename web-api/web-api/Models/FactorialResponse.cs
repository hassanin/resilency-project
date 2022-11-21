namespace web_api.Models
{
    public class FactorialResponse
    {
        public required int Result { get; init; }
        public required string version { get; init; }
        public DateTime Ttl { get; init; } = DateTime.UtcNow.AddHours(1);
    }
}
