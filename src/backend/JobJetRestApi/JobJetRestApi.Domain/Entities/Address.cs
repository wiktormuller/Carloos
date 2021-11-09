namespace JobJetRestApi.Domain.Entities
{
    public class Address
    {
        public int Id { get; private set; } // Without private setter it can be only set via constructor but not by class methods
        public CountryIso CountryIso { get; private set; }
        public string Town { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }
    }
}