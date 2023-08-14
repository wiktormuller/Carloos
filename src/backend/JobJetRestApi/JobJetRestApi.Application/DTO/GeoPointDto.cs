using System;

namespace JobJetRestApi.Application.DTO
{
    public class GeoPointDto
    {
        public decimal Longitude { get; private set; }
        public decimal Latitude { get; private set; }
        
        public GeoPointDto(decimal longitude, decimal latitude)
        {
            if (longitude < -180 || longitude > 180) //<-180; 180°>
            {
                throw new ArgumentException(nameof(longitude));
            }

            if (latitude < -90 || latitude > 90) // <-90; 90°>
            {
                throw new ArgumentException(nameof(latitude));
            }
            
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}