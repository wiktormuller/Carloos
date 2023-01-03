using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace JobJetRestApi.Domain.Entities
{
    public class TechnologyType
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        
        // Relationships
        public ICollection<JobOffer> JobOffers { get; set; }

        private TechnologyType() {} // For EF purposes
        
        public TechnologyType(string name)
        {
            Name = Guard.Against.Null(name, nameof(name));
        }

        public void UpdateName(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
        }
    }
}