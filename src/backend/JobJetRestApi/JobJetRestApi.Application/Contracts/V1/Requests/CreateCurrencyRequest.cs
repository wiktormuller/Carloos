namespace JobJetRestApi.Application.Contracts.V1.Requests
{
    public class CreateCurrencyRequest
    {
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public int IsoNumber { get; set; }
    }
}