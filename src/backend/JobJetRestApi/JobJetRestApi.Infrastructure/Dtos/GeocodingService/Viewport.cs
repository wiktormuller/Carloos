using System.Text.Json.Serialization;

namespace JobJetRestApi.Infrastructure.Dtos.GeocodingService
{
    public class Viewport
    {
        [JsonPropertyName("northeast")]
        public Northeast Northeast { get; set; }

        [JsonPropertyName("southwest")]
        public Southwest Southwest { get; set; }
    }
}