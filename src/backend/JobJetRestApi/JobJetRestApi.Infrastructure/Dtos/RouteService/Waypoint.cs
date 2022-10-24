using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JobJetRestApi.Infrastructure.Dtos.RouteService
{
    public class Waypoint
    {
        [JsonPropertyName("hint")]
        public string Hint { get; set; }

        [JsonPropertyName("distance")]
        public double Distance { get; set; }

        [JsonPropertyName("location")]
        public List<double> Location { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}