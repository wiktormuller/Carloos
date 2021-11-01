namespace JobJetRestApi.Domain.Entities
{
    public class CountryISO
    {
        public int Id { get; }
        public string Name { get; }
        public string Alpha2Code { get; }
        public string Alpha3Code { get; }
        public int NumericCode { get; }
    }
}