using backend.Enums;
using System.Text.Json.Serialization;

namespace backend.DTOs
{
    public class RatingDTO
    {
        public long RatingId { get; set; }

        // S is enum value 0; without this the global WhenWritingDefault would omit it
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Ratings Ratings { get; set; }
        public string? RatingUser { get; set; }
        public HeroDTO Hero { get; set; } = null!;
    }
}