using backend.Enums;

namespace backend.DTOs
{
    public class TeamDTO
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string? TeamImage { get; set; }
        public Regions CompetingRegion { get; set; }
    }
}