using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JobJetRestApi.Infrastructure.Dtos.GeocodingService
{
    public class Root
    {
        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}