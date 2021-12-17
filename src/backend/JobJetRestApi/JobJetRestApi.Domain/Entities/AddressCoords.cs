namespace JobJetRestApi.Domain.Entities
{
    public class AddressCoords // Where to store this class?
    {
        public string Address { get; private set; }
        public decimal Longitude { get; private set; }
        public decimal Latitude { get; private set; }
        
        

        public AddressCoords(string address, decimal longitude, decimal latitude)
        {
            Address = address;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}