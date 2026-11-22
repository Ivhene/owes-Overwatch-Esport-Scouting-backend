using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.DTOs
{
    /// <summary>
    /// A single rating given to a player on a single hero.
    /// </summary>
    public sealed class RatePlayerRequest
    {
        [Range(1, int.MaxValue)]
        [JsonPropertyName("hero")]
        public int HeroId { get; set; }

        // Nullable so a missing rating fails validation instead of defaulting to S
        [Required]
        [JsonPropertyName("rating")]
        public backend.Enums.Ratings? Rating { get; set; }

        [JsonPropertyName("rating_user")]
        public string? RatingUser { get; set; }
    }
}
