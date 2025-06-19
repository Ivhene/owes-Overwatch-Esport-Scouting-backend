using backend.Enums;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace backend.Models
{
    [Table("player")]
    public partial class Player : BaseModel
    {
        [PrimaryKey("player_id", true)]
        [Column("player_id")]
        [JsonPropertyName("player_id")]
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonProperty("player_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int PlayerId { get; set; }

        [Column("gamertag")]
        [JsonPropertyName("gamertag")]
        public string Gamertag { get; set; } = null!;

        [Column("real_name")]
        [JsonPropertyName("real_name")]
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RealName { get; set; }

        [Column("birthday")]
        [JsonPropertyName("birthday")]
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateOnly? Birthday { get; set; }

        [Column("player_role")]
        [JsonPropertyName("player_role")]
        public int PlayerRole { get; set; }

        [Column("native_region")]
        [JsonPropertyName("native_region")]
        public Regions NativeRegion { get; set; }

        [Column("player_image")]
        [JsonPropertyName("player_image")]
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PlayerImage { get; set; }

        [Column("current_team")]
        [JsonPropertyName("current_team")]
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CurrentTeam { get; set; }
    }
}
