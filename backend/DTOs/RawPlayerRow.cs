using backend.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace backend.DTOs
{
    public class RawPlayerRow
    {
        [JsonProperty("player_id")] public int PlayerId { get; set; }
        [JsonProperty("gamertag")] public string Gamertag { get; set; } = null!;
        [JsonProperty("real_name")] public string? RealName { get; set; }
        [JsonProperty("birthday")] public DateOnly? Birthday { get; set; }
        [JsonProperty("native_region")] public Regions NativeRegion { get; set; }
        [JsonProperty("player_image")] public string? PlayerImage { get; set; }

        // these _names_ must exactly match your select aliases:
        [JsonProperty("role")] public RawRole Role { get; set; } = null!;
        [JsonProperty("current_team")] public RawTeam? CurrentTeam { get; set; }
        [JsonProperty("ratings")] public List<RawRating> Ratings { get; set; } = new();
    }

    public class RawRole
    {
        [JsonProperty("role_id")] public int RoleId { get; set; }
        [JsonProperty("role_name")] public string RoleName { get; set; } = null!;
        [JsonProperty("role_image")] public string RoleImage { get; set; } = null!;
    }

    public class RawTeam
    {
        [JsonProperty("team_id")] public int TeamId { get; set; }
        [JsonProperty("team_name")] public string TeamName { get; set; } = null!;
        [JsonProperty("team_image")] public string? TeamImage { get; set; }
        [JsonProperty("competing_region")] public Regions CompetingRegion { get; set; }
    }

    public class RawRating
    {
        [JsonProperty("rating_id")] public long RatingId { get; set; }
        [JsonProperty("rating")] public Ratings Ratings { get; set; }
        [JsonProperty("rating_user")] public string? RatingUser { get; set; }
        [JsonProperty("hero")] public RawHero Hero { get; set; } = null!;
    }

    public class RawHero
    {
        [JsonProperty("hero_id")] public int HeroId { get; set; }
        [JsonProperty("hero_name")] public string HeroName { get; set; } = null!;
        [JsonProperty("hero_image")] public string HeroImage { get; set; } = null!;
        [JsonProperty("hero_role")] public int HeroRole { get; set; }
    }
}
