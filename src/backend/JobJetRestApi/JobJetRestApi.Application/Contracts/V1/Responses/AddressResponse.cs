namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class AddressResponse
    {
        public int Id { get; private set; }
        public string CountryName { get; private set; }
        public string Town { get; private set; }
        public string ZipCode { get; private set; }
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }

        public AddressResponse(int id, string countryName, string town, 
            string zipCode, decimal latitude, decimal longitude)
        {
            Id = id;
            CountryName = countryName;
            Town = town;
            ZipCode = zipCode;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}