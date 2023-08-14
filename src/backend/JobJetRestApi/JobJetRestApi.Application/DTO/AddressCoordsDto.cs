namespace JobJetRestApi.Application.DTO
{
    public class AddressCoordsDto
    {
        public string Address { get; private set; }
        public decimal Longitude { get; private set; }
        public decimal Latitude { get; private set; }
        
        

        public AddressCoordsDto(string address, decimal longitude, decimal latitude)
        {
            Address = address;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}