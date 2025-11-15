using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using backend.DTOs;

namespace backend.DTOs
{
    [Table("hero")]
    public class RawHeroRow : BaseModel
    {
        [PrimaryKey("hero_id", true)]
        [Column("hero_id")]
        [JsonProperty("hero_id")]
        public int HeroId { get; set; }

        [Column("hero_name")]
        [JsonProperty("hero_name")]
        public string HeroName { get; set; } = null!;

        [Column("hero_image")]
        [JsonProperty("hero_image")]
        public string HeroImage { get; set; } = null!;

        // Keep the FK as well in case it's needed; it will still be populated by DB.
        [Column("hero_role")]
        [JsonProperty("hero_role")]
        public int HeroRole { get; set; }

        // top-level nested role object for the hero (role:role (...))
        [JsonProperty("role")]
        public RawRole? Role { get; set; }

        // ratings:rating (... player:player (... role:role (...)))
        [JsonProperty("ratings")]
        public List<RawRatingForHero> Ratings { get; set; } = new();
    }

    public class RawRatingForHero
    {
        [JsonProperty("rating_id")]
        public long RatingId { get; set; }

        [JsonProperty("rating")]
        public backend.Enums.Ratings Ratings { get; set; }

        [JsonProperty("rating_user")]
        public string? RatingUser { get; set; }

        [JsonProperty("player")]
        public RawPlayerForRating Player { get; set; } = null!;
    }

    public class RawPlayerForRating
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("gamertag")]
        public string Gamertag { get; set; } = null!;

        [JsonProperty("real_name")]
        public string? RealName { get; set; }

        [JsonProperty("birthday")]
        public DateOnly? Birthday { get; set; }

        [JsonProperty("native_region")]
        public backend.Enums.Regions NativeRegion { get; set; }

        [JsonProperty("player_image")]
        public string? PlayerImage { get; set; }

        [JsonProperty("player_role")]
        public int PlayerRole { get; set; }

        [JsonProperty("current_team")]
        public int? CurrentTeam { get; set; }

        // include nested role object for the player (already used previously)
        [JsonProperty("role")]
        public RawRole Role { get; set; } = null!;
    }
}