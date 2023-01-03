using System.Collections.Generic;
using Ardalis.GuardClauses;
using JobJetRestApi.Domain.Exceptions;

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
            Country = Guard.Against.Null(country, nameof(country));
            Town = Guard.Against.NullOrEmpty(town, nameof(town));
            Street = Guard.Against.NullOrEmpty(street, nameof(street));
            ZipCode = Guard.Against.NullOrEmpty(zipCode, nameof(zipCode));
            Latitude = ValidateLatitude(latitude);
            Longitude = ValidateLongitude(longitude);
        }
        
        /// <exception cref="IncorrectCoordsException"></exception>
        private decimal ValidateLatitude(decimal latitude)
        {
            if (latitude < -90 || latitude > 90)
            {
                throw IncorrectCoordsException.ForLatitude();
            }

            return latitude;
        }
        
        /// <exception cref="IncorrectCoordsException"></exception>
        private decimal ValidateLongitude(decimal longitude)
        {
            if (longitude < -180 || longitude > 180)
            {
                throw IncorrectCoordsException.ForLongitude();
            }

            return longitude;
        }
    }
}