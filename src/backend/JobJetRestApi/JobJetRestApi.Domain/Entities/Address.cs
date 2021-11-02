namespace JobJetRestApi.Domain.Entities
{
    public class Address
    {
        public int Id { get; }
        public CountryIso CountryIso { get; }
        public string Town { get; }
        public string Street { get; }
        public string ZipCode { get; }
        public decimal Latitude { get; }
        public decimal Longitude { get; }
    }
}