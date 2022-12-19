namespace JobJetRestApi.Application.Contracts.V1.Requests
{
    public class CreateAddressRequest
    {
        public string Town { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public int CountryId { get; set; }
    }
}