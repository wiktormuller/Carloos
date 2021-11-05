namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public string CountryIso { get; set; }
        public string Town { get; set; }
        public string ZipCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}