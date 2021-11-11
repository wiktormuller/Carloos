using System.Collections.Generic;

namespace JobJetRestApi.Domain.Entities
{
    public class Currency
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string IsoCode { get; set; } 
        public int IsoNumber { get; set; }
        
        // Relationships
        public ICollection<JobOffer> JobOffers { get; private set; }
        
        private Currency() {}

        public Currency(string name, string isoCode, int isoNumber)
        {
            Name = name;
            IsoCode = isoCode;
            IsoNumber = isoNumber;
        }
    }
}