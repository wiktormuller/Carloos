using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace JobJetRestApi.Domain.Entities
{
    public class Currency
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string IsoCode { get; private set; } 
        public int IsoNumber { get; private set; }
        
        // Relationships
        public ICollection<JobOffer> JobOffers { get; private set; }
        
        private Currency() {}

        public Currency(string name, string isoCode, int isoNumber)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
            IsoCode = Guard.Against.NullOrEmpty(isoCode, nameof(isoCode));
            IsoNumber = Guard.Against.NegativeOrZero(isoNumber, nameof(isoNumber));
        }

        public void UpdateName(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
        }
    }
}