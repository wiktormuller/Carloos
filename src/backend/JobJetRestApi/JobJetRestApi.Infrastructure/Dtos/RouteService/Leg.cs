using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JobJetRestApi.Infrastructure.Dtos.RouteService
{
    public class Leg
    {
        [JsonPropertyName("steps")]
        public List<object> Steps { get; set; }

        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        [JsonPropertyName("distance")]
        public double Distance { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("duration")]
        public double Duration { get; set; }
    }
}