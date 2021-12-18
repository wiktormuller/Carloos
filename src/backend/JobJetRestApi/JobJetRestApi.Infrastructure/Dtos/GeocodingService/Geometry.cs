using System.Text.Json.Serialization;

namespace JobJetRestApi.Infrastructure.Dtos.GeocodingService
{
    public class Geometry
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("location_type")]
        public string LocationType { get; set; }

        [JsonPropertyName("viewport")]
        public Viewport Viewport { get; set; }
    }
}