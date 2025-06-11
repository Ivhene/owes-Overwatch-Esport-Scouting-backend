using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend.Models
{
    [Table("hero")]
    public partial class Hero : BaseModel
    {
        [PrimaryKey("hero_id", false)]
        [JsonPropertyName("hero_id")]
        public int HeroId { get; set; }

        [JsonPropertyName("hero_name")]
        public string HeroName { get; set; } = null!;

        [JsonPropertyName("hero_image")]
        public string HeroImage { get; set; } = null!;

        [JsonPropertyName("hero_role")]
        public int HeroRole { get; set; }
    }
}
