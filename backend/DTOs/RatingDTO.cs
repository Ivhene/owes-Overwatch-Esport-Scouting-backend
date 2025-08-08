using backend.Enums;

namespace backend.DTOs
{
    public class RatingDTO
    {
        public long RatingId { get; set; }
        public Ratings Ratings { get; set; }
        public string? RatingUser { get; set; }
        public HeroDTO Hero { get; set; } = null!;
    }
}