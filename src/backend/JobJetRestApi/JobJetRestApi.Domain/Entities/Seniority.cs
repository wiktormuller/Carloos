using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace JobJetRestApi.Domain.Entities
{
    public class Seniority
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        
        // Relationships
        public ICollection<JobOffer> JobOffers { get; set; }

        private Seniority() {} // For EF purposes
        
        public Seniority(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
        }

        public void UpdateName(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
        }
    }
}