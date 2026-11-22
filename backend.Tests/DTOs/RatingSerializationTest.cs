using System.Text.Json;
using System.Text.Json.Serialization;
using backend.DTOs;
using backend.Enums;

namespace backend.Tests.DTOs
{
    public class RatingSerializationTests
    {
        // Mirrors the global API JSON options configured in Program.cs
        private static readonly JsonSerializerOptions ApiOptions = new()
        {
            PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy(),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            IgnoreReadOnlyProperties = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        [Fact]
        public void RatingDTO_WritesSRating()
        {
            string json = JsonSerializer.Serialize(new RatingDTO { RatingId = 9, Ratings = Ratings.S }, ApiOptions);

            Assert.Contains("\"ratings\":\"s\"", json);
        }

        [Fact]
        public void RatingWithPlayerDTO_WritesSRating()
        {
            string json = JsonSerializer.Serialize(new RatingWithPlayerDTO { RatingId = 9, Ratings = Ratings.S }, ApiOptions);

            Assert.Contains("\"ratings\":\"s\"", json);
        }
    }
}
