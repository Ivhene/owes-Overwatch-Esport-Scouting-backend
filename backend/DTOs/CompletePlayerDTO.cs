using backend.Enums;
using System.Text.Json.Serialization;

namespace backend.DTOs
{
    public class CompletePlayerDTO
    {
        public int PlayerId { get; set; }
        public string Gamertag { get; set; } = null!;
        public string? RealName { get; set; }
        public DateOnly? Birthday { get; set; }

        // NA is enum value 0; without this the global WhenWritingDefault would omit it
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Regions NativeRegion { get; set; }
        public string? PlayerImage { get; set; }

        public RoleDTO Role { get; set; } = null!;
        public TeamDTO? CurrentTeam { get; set; }

        public List<RatingDTO> Ratings { get; set; } = new();
    }
}
