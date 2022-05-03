namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class AddressResponse
    {
        public string CountryName { get; private set; }
        public string Town { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }

        public AddressResponse(string countryName, string town, string street,
            string zipCode, decimal latitude, decimal longitude)
        {
            CountryName = countryName;
            Town = town;
            Street = street;
            ZipCode = zipCode;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}