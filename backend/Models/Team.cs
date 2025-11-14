using backend.Enums;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace backend.Models
{
    [Table("team")]
    public partial class Team : BaseModel
    {
        [PrimaryKey("team_id", true)]
        [Column("team_id")]
        [JsonPropertyName("team_id")]
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonProperty("team_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int TeamId { get; set; }

        [Column("team_name")]
        [JsonPropertyName("team_name")]
        public string TeamName { get; set; } = null!;

        [Column("team_image")]
        [JsonPropertyName("team_image")]
        public string? TeamImage { get; set; }

        [Column("competing_region")]
        [JsonPropertyName("competing_region")]
        public Regions CompetingRegion { get; set; }
    }
}
