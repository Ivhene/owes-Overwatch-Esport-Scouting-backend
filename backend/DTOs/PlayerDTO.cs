using backend.Enums;
using System.Text.Json.Serialization;

namespace backend.DTOs
{
    public class PlayerDTO
    {
        [JsonPropertyName("player_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int PlayerId { get; set; }

        [JsonPropertyName("gamertag")]
        public string Gamertag { get; set; } = null!;

        [JsonPropertyName("real_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RealName { get; set; }

        [JsonPropertyName("birthday")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateOnly? Birthday { get; set; }

        [JsonPropertyName("player_role")]
        public int PlayerRole { get; set; }

        [JsonPropertyName("native_region")]
        public Regions NativeRegion { get; set; }

        [JsonPropertyName("player_image")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PlayerImage { get; set; }

        [JsonPropertyName("current_team")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CurrentTeam { get; set; }
    }
}
