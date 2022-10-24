using System.Text.Json.Serialization;

namespace JobJetRestApi.Infrastructure.Dtos.GeocodingService
{
    public class PlusCode
    {
        [JsonPropertyName("compound_code")]
        public string CompoundCode { get; set; }

        [JsonPropertyName("global_code")]
        public string GlobalCode { get; set; }
    }
}