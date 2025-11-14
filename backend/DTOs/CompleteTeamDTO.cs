using backend.Enums;
using System.Collections.Generic;

namespace backend.DTOs
{
    public class CompleteTeamDTO
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string? TeamImage { get; set; }
        public Regions CompetingRegion { get; set; }
        public List<CompletePlayerDTO> Players { get; set; } = new();
    }
}