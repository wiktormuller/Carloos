using System.Collections.Generic;

namespace JobJetRestApi.Domain.Entities
{
    public class Address
    {
        public int Id { get; private set; } // Without private setter it can be only set via constructor but not by class methods
        public string Town { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }
        
        // Relationships
        public Country Country { get; private set; }
        public int CountryId { get; set; } // Non nullable relationship
        public ICollection<JobOffer> JobOffers { get; set; }

        private Address() {} // For EF purposes
        
        public Address(Country country, string town, string street, string zipCode, decimal latitude, decimal longitude)
        {
            Country = country;
            Town = town;
            Street = street;
            ZipCode = zipCode;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}