namespace JobJetRestApi.Infrastructure.Options
{
    public class GeocodingOptions
    {
        public const string Geocoding = "Geocoding";
        
        public string BaseUri { get; set; }
        public string ApiKey { get; set; }
    }
}