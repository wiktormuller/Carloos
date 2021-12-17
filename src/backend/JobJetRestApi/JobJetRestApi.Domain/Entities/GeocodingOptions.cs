namespace JobJetRestApi.Domain.Entities
{
    public class GeocodingOptions
    {
        public const string Geocoding = "Geocoding";
        
        public string BaseUri { get; set; }
        public string ApiKey { get; set; }
    }
}