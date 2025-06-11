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

        [JsonPropertyName("hero")]
        public int HeroId { get; set; }

        [JsonPropertyName("player")]
        public int PlayerId { get; set; }

        [JsonPropertyName("rating_user")]
        public string? RatingUser { get; set; }
    }
}
