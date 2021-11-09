namespace JobJetRestApi.Domain.Entities
{
    public class Country
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Alpha2Code { get; private set; }
        public string Alpha3Code { get; private set; }
        public int NumericCode { get; private set; }
    }
}