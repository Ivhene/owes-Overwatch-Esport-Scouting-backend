using backend.Enums;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models
{
    [Table("team")]
    public partial class Team : BaseModel
    {
        [PrimaryKey("team_id", false)]
        [JsonPropertyName("team_id")]
        public int TeamId { get; set; }

        [JsonPropertyName("team_name")]
        public string TeamName { get; set; } = null!;

        [JsonPropertyName("team_image")]
        public string? TeamImage { get; set; }

        [JsonPropertyName("competing_region")]
        public Regions CompetingRegion { get; set; }
    }
}
