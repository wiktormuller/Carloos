using System.Collections.Generic;
using Ardalis.GuardClauses;
using JobJetRestApi.Domain.Exceptions;

namespace JobJetRestApi.Domain.Entities
{
    public class Country
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Alpha2Code { get; private set; }
        public string Alpha3Code { get; private set; }
        public int NumericCode { get; private set; }
        public decimal LatitudeOfCapital { get; private set; }
        public decimal LongitudeOfCapital { get; private set; }

        // Relationships
        public ICollection<Address> Addresses { get; set; }

        private Country() {} // For EF purposes
        
        public Country(string name, string alpha2Code, string alpha3Code, int numericCode, 
            decimal latitudeOfCapital, decimal longitudeOfCapital)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
            Alpha2Code = Guard.Against.NullOrEmpty(alpha2Code, nameof(alpha2Code));
            Alpha3Code = Guard.Against.NullOrEmpty(alpha3Code, nameof(alpha3Code));
            NumericCode = Guard.Against.NegativeOrZero(numericCode, nameof(numericCode));
            LatitudeOfCapital = ValidateLatitude(latitudeOfCapital);
            LongitudeOfCapital = ValidateLongitude(longitudeOfCapital);
        }

        public void UpdateName(string name)
        {
            Name = name;
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