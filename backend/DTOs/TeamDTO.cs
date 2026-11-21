using backend.Enums;
using System.Text.Json.Serialization;

namespace backend.DTOs
{
    public class TeamDTO
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string? TeamImage { get; set; }

        // NA is enum value 0; without this the global WhenWritingDefault would omit it
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Regions CompetingRegion { get; set; }
    }
}