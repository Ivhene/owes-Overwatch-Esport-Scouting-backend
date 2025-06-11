using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models
{
    [Table("player")]
    public partial class Player : BaseModel
    {
        [PrimaryKey("player_id", false)]
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("gamertag")]
        public string Gamertag { get; set; } = null!;

        [JsonPropertyName("real_name")]
        public string? RealName { get; set; }

        [JsonPropertyName("birthday")]
        public DateOnly? Birthday { get; set; }

        [JsonPropertyName("player_role")]
        public int PlayerRole { get; set; }

        [JsonPropertyName("player_image")]
        public string? PlayerImage { get; set; }

        [JsonPropertyName("current_team")]
        public int? CurrentTeam { get; set; }
    }
}
