using backend.Enums;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace backend.DTOs
{
    [Table("player")]
    public class RawPlayerRow : BaseModel
    {
        [PrimaryKey("player_id", true)]
        [Column("player_id")]
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [Column("gamertag")]
        [JsonProperty("gamertag")]
        public string Gamertag { get; set; } = null!;

        [Column("real_name")]
        [JsonProperty("real_name")]
        public string? RealName { get; set; }

        [Column("birthday")]
        [JsonProperty("birthday")]
        public DateOnly? Birthday { get; set; }

        [Column("native_region")]
        [JsonProperty("native_region")]
        public Regions NativeRegion { get; set; }

        [Column("player_image")]
        [JsonProperty("player_image")]
        public string? PlayerImage { get; set; }

        [JsonProperty("role")]
        public RawRole Role { get; set; } = null!;

        [JsonProperty("current_team")]
        public RawTeam? CurrentTeam { get; set; }

        [JsonProperty("ratings")]
        public List<RawRating> Ratings { get; set; } = new();
    }

    public class RawRole
    {
        [JsonProperty("role_id")]
        public int RoleId { get; set; }

        [JsonProperty("role_name")]
        public string RoleName { get; set; } = null!;

        [JsonProperty("role_image")]
        public string RoleImage { get; set; } = null!;
    }

    public class RawTeam
    {
        [JsonProperty("team_id")]
        public int TeamId { get; set; }

        [JsonProperty("team_name")]
        public string TeamName { get; set; } = null!;

        [JsonProperty("team_image")]
        public string? TeamImage { get; set; }

        [JsonProperty("competing_region")]
        public Regions CompetingRegion { get; set; }
    }

    public class RawRating
    {
        [JsonProperty("rating_id")]
        public long RatingId { get; set; }

        [JsonProperty("rating")]
        public Ratings Ratings { get; set; }

        [JsonProperty("rating_user")]
        public string? RatingUser { get; set; }

        [JsonProperty("hero")]
        public RawHero Hero { get; set; } = null!;
    }

    public class RawHero
    {
        [JsonProperty("hero_id")]
        public int HeroId { get; set; }

        [JsonProperty("hero_name")]
        public string HeroName { get; set; } = null!;

        [JsonProperty("hero_image")]
        public string HeroImage { get; set; } = null!;

        [JsonProperty("hero_role")]
        public int HeroRole { get; set; }
    }
}
