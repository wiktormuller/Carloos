namespace JobJetRestApi.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public CountryISO CountryISO { get; }
        public string Town { get; }
        public string Street { get; }
        public string ZipCode { get; }
    }
}