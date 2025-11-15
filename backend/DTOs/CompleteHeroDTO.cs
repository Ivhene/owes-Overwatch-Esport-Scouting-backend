using backend.Enums;
using System.Collections.Generic;

namespace backend.DTOs
{
    public class CompleteHeroDTO
    {
        public int HeroId { get; set; }
        public string HeroName { get; set; } = null!;
        public string HeroImage { get; set; } = null!;

        // replaced HeroRole int with full RoleDTO for the hero
        public RoleDTO Role { get; set; } = null!;

        // each rating includes the player who made it
        public List<RatingWithPlayerDTO> Ratings { get; set; } = new();
    }

    public class RatingWithPlayerDTO
    {
        public long RatingId { get; set; }
        public Ratings Ratings { get; set; }
        public string? RatingUser { get; set; }
        public PlayerDTO Player { get; set; } = null!;
    }
}