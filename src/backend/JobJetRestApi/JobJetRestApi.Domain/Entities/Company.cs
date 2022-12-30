using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using JobJetRestApi.Domain.Exceptions;

namespace JobJetRestApi.Domain.Entities
{
    public class Company
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public string Description { get; private set; }
        public int NumberOfPeople { get; private set; }
        public string CityName { get; private set; }
        public List<JobOffer> JobOffers { get; private set; } = new List<JobOffer>();

        private Company() {}

        public Company(string name, string shortName, string description, int numberOfPeople, string cityName)
        {
            Name = name;
            ShortName = shortName;
            Description = description;
            NumberOfPeople = numberOfPeople;
            CityName = cityName;
        }
        
        /// <exception cref="CannotDeleteJobOfferException"></exception>
        public void DeleteJobOffer(int jobOfferId)
        {
            Guard.Against.NegativeOrZero(jobOfferId, nameof(jobOfferId));
            
            if (!IsOwnerOfJobOffer(jobOfferId))
            {
                throw CannotDeleteJobOfferException.YouAreNotJobOfferOwner(jobOfferId);
            }

            JobOffers.RemoveAll(jobOffer => jobOffer.Id == jobOfferId);
        }
        
        /// <exception cref="CompanyCannotHaveJobOffersWithTheSameNamesException"></exception>
        public void AddJobOffer(JobOffer jobOffer)
        {
            Guard.Against.Null(jobOffer, nameof(jobOffer));
            
            if (JobOffers.Any(existingJobOffer => existingJobOffer.Name == jobOffer.Name))
            {
                throw CompanyCannotHaveJobOffersWithTheSameNamesException.ForName(jobOffer.Name);
            }

            JobOffers.Add(jobOffer);
        }

        public bool IsOwnerOfJobOffer(int jobOfferId)
        {
            return JobOffers.Any(jobOffer => jobOffer.Id == jobOfferId);
        }
        
        /// <exception cref="CannotUpdateJobOfferException"></exception>
        public void UpdateJobOffer(int jobOfferId, string name, string description, decimal salaryFrom, decimal salaryTo)
        {
            Guard.Against.NegativeOrZero(jobOfferId, nameof(jobOfferId));
            Guard.Against.Null(name, nameof(name));
            Guard.Against.Null(description, nameof(description));
            Guard.Against.NegativeOrZero(salaryFrom, nameof(salaryFrom));
            Guard.Against.NegativeOrZero(salaryTo, nameof(salaryTo));
            
            if (!IsOwnerOfJobOffer(jobOfferId))
            {
                throw CannotUpdateJobOfferException.YouAreNotJobOfferOwner(jobOfferId);
            }
            
            JobOffers.First(jobOffer => jobOffer.Id == jobOfferId)
                .UpdateBasicInformation(name, description, salaryFrom, salaryTo);
        }
        
        public void Update(string description, int numberOfPeople)
        {
            Guard.Against.Null(description, nameof(description));
            Guard.Against.Null(numberOfPeople, nameof(numberOfPeople));
            
            Description = description;
            NumberOfPeople = numberOfPeople;
        }
    }
}