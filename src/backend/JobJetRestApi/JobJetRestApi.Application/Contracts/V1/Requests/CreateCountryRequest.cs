namespace JobJetRestApi.Application.Contracts.V1.Requests
{
    public class CreateCountryRequest
    {
        public string Name { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public int NumericCode { get; set; }
    }
}