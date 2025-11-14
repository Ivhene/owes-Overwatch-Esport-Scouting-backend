using backend.Enums;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace backend.DTOs
{
    [Table("team")]
    public class RawTeamRow : BaseModel
    {
        [PrimaryKey("team_id", true)]
        [Column("team_id")]
        [JsonProperty("team_id")]
        public int TeamId { get; set; }

        [Column("team_name")]
        [JsonProperty("team_name")]
        public string TeamName { get; set; } = null!;

        [Column("team_image")]
        [JsonProperty("team_image")]
        public string? TeamImage { get; set; }

        [Column("competing_region")]
        [JsonProperty("competing_region")]
        public Regions CompetingRegion { get; set; }

        // This will be populated by the nested select of "players:player (...)"
        [JsonProperty("players")]
        public List<RawPlayerRow> Players { get; set; } = new();
    }
}