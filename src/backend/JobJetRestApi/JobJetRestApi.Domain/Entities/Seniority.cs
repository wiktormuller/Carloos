﻿using System.Collections.Generic;

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
            Name = name;
        }
    }
}