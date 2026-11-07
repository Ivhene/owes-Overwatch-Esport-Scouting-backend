using backend.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.DTOs
{
    public sealed class UpdatePlayerRequest
    {
        [JsonPropertyName("gamertag")]
        public string Gamertag { get; set; } = null!;

        [JsonPropertyName("real_name")]
        public string? RealName { get; set; }

        [JsonPropertyName("birthday")]
        public DateOnly? Birthday { get; set; }

        [JsonPropertyName("player_role")]
        public int PlayerRole { get; set; }

        [JsonPropertyName("native_region")]
        public Regions NativeRegion { get; set; }

        [JsonPropertyName("player_image")]
        public string? PlayerImage { get; set; }

        [JsonPropertyName("current_team")]
        public int? CurrentTeam { get; set; }

        [JsonPropertyName("ratings")]
        public List<UpdatePlayerRatingRequest> Ratings { get; set; } = [];
    }

    public sealed class UpdatePlayerRatingRequest
    {
        [JsonPropertyName("rating_id")]
        public int RatingId { get; set; }

        [JsonPropertyName("ratings")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public backend.Enums.Ratings Ratings { get; set; }

        [Required]
        [JsonPropertyName("hero")]
        public UpdateRatingHeroRequest Hero { get; set; } = null!;
    }

    public sealed class UpdateRatingHeroRequest
    {
        [Range(1, int.MaxValue)]
        [JsonPropertyName("hero_id")]
        public int HeroId { get; set; }

        [JsonPropertyName("hero_name")]
        public string? HeroName { get; set; }

        [JsonPropertyName("hero_image")]
        public string? HeroImage { get; set; }

        [JsonPropertyName("hero_role")]
        public int? HeroRole { get; set; }
    }
}