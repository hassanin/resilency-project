using System.ComponentModel.DataAnnotations;

namespace ResilencyClient.Config
{
    public class ApplicationConfig
    {
        [Required]
        public required Uri PrimaryBackendUrl { get; init; }
        [Required]
        public required Uri SecondaryBackendUrl { get; init; }
    }
}
