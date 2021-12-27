using System.Collections.Generic;

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
            Name = name;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}