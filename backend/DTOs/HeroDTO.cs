namespace backend.DTOs
{
    public class HeroDTO
    {
        public int HeroId { get; set; }
        public string HeroName { get; set; } = null!;
        public string HeroImage { get; set; } = null!;
        public int HeroRole { get; set; }
    }
}