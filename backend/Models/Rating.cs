using backend.Enums;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Text.Json.Serialization;

namespace backend.Models
{
    [Table("rating")]
    public partial class Rating : BaseModel
    {
        [PrimaryKey("rating_id", false)]
        [JsonPropertyName("rating_id")]
        public long RatingId { get; set; }

        [Column("rating")]
        [JsonPropertyName("rating")]
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Ratings Ratings { get; set; }

        [JsonPropertyName("hero")]
        public int HeroId { get; set; }

        [JsonPropertyName("player")]
        public int PlayerId { get; set; }

        [JsonPropertyName("rating_user")]
        public string? RatingUser { get; set; }
    }
}
