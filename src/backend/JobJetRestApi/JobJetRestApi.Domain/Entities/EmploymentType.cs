using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace JobJetRestApi.Domain.Entities
{
    public class EmploymentType
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        
        // Relationships
        public ICollection<JobOffer> JobOffers { get; set; }

        private EmploymentType() {} // For EF purposes
        
        public EmploymentType(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
        }

        public void UpdateName(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
        }
    }
}