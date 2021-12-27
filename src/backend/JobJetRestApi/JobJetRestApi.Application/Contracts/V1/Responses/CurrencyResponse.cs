namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class CurrencyResponse
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public string IsoCode { get; private set; }

        public CurrencyResponse(int id, string name, string isoCode)
        {
            Id = id;
            Name = name;
            IsoCode = isoCode;
        }
    }
}