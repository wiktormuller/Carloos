using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JobJetRestApi.Infrastructure.Dtos.RouteService
{
    public class Geometry
    {
        [JsonPropertyName("coordinates")]
        public List<List<double>> Coordinates { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}