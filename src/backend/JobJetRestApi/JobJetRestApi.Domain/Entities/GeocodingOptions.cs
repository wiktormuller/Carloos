namespace JobJetRestApi.Domain.Entities
{
    public class GeocodingOptions //@TODO - Remove it from here
    {
        public const string Geocoding = "Geocoding";
        
        public string BaseUri { get; set; }
        public string ApiKey { get; set; }
    }
}