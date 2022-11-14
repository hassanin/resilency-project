using System.ComponentModel.DataAnnotations;

namespace web_api.Config
{
    public class ResponseProfile
    {
        [Required]
        public required int NumRequests { get; init; }
        [Required]
        public required int ResponseTimeMilliSeconds { get; init; }
        [Required]
        [Range(0, 100)]
        public required int Percentile { get; init; }
    }
}
