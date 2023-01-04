using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using JobJetRestApi.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace JobJetRestApi.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public List<Company> Companies { get; private set; } = new List<Company>();

        public List<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

        private User() : base()
        {
        }
        
        public User(string email, string username)
        {
            Email = Guard.Against.NullOrEmpty(email, nameof(email));
            UserName = Guard.Against.NullOrEmpty(username, nameof(username));
        }

        public void AddRefreshToken(RefreshToken refreshToken)
        {
            Guard.Against.Null(refreshToken, nameof(refreshToken));
            
            RefreshTokens.Add(refreshToken);
        }

        /// <exception cref="CannotDeleteJobOfferException"></exception>
        public void DeleteJobOffer(int jobOfferId)
        {
            Guard.Against.NegativeOrZero(jobOfferId, nameof(jobOfferId));
            
            if (!IsOwnerOfJobOffer(jobOfferId))
            {
                throw CannotDeleteJobOfferException.YouAreNotJobOfferOwner(jobOfferId);
            }
            
            Companies.First(company => company.JobOffers.Any(jobOffer => jobOffer.Id == jobOfferId))
                .DeleteJobOffer(jobOfferId);
        }
        
        /// <exception cref="CannotUpdateJobOfferException"></exception>
        public void UpdateJobOffer(int jobOfferId, string name, string description, decimal salaryFrom,
            decimal salaryTo)
        {
            Guard.Against.NegativeOrZero(jobOfferId, nameof(jobOfferId));
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NullOrEmpty(description, nameof(description));
            Guard.Against.NegativeOrZero(salaryFrom, nameof(salaryFrom));
            Guard.Against.NegativeOrZero(salaryTo, nameof(salaryTo));

            if (!IsOwnerOfJobOffer(jobOfferId))
            {
                throw CannotUpdateJobOfferException.YouAreNotJobOfferOwner(jobOfferId);
            }
            
            Companies.First(company => company.JobOffers.Any(jobOffer => jobOffer.Id == jobOfferId))
                .UpdateJobOffer(jobOfferId, name, description, salaryFrom, salaryTo);
        }
        
        /// <exception cref="CannotDeleteCompanyInformationException"></exception>
        public void DeleteCompany(int companyId)
        {
            Guard.Against.NegativeOrZero(companyId, nameof(companyId));
            
            if (!IsOwnerOfCompany(companyId))
            {
                throw CannotDeleteCompanyInformationException.YouAreNotCompanyOwner(companyId);
            }

            Companies.RemoveAll(company => company.Id == companyId);
        }
        
        /// <exception cref="CannotUpdateCompanyInformationException"></exception>
        public void UpdateCompanyInformation(int companyId, string description, int numberOfPeople)
        {
            Guard.Against.NegativeOrZero(companyId, nameof(companyId));
            Guard.Against.NullOrEmpty(description, nameof(description));
            Guard.Against.NegativeOrZero(numberOfPeople, nameof(numberOfPeople));
            
            if (!IsOwnerOfCompany(companyId))
            {
                throw CannotUpdateCompanyInformationException.YouAreNotCompanyOwner(companyId);
            }

            Companies.First(company => company.Id == companyId).Update(description, numberOfPeople);
        }
        
        /// <exception cref="CannotCreateJobOfferException"></exception>
        public void AddJobOffer(Company company, JobOffer jobOffer)
        {
            Guard.Against.Null(company, nameof(company));
            Guard.Against.Null(jobOffer, nameof(jobOffer));
            
            if (!IsOwnerOfCompany(company.Id))
            {
                throw CannotCreateJobOfferException.YouAreNotCompanyOwner(company.Id);
            }

            Companies
                .First(existingCompany => existingCompany.Id == company.Id)
                .AddJobOffer(jobOffer);
        }
        
        /// <exception cref="UserCannotHaveCompaniesWithTheSameNamesException"></exception>
        public void AddCompany(Company company)
        {
            Guard.Against.Null(company);
            
            if (Companies.Any(existingCompany => existingCompany.Name == company.Name))
            {
                throw UserCannotHaveCompaniesWithTheSameNamesException.ForName(company.Name);
            }

            Companies.Add(company);
        }

        public void UpdateUsername(string username)
        {
            UserName = Guard.Against.NullOrEmpty(username, nameof(username));
        }
        
        private bool IsOwnerOfCompany(int companyId)
        {
            return Companies.Any(company => company.Id == companyId);
        }

        private bool IsOwnerOfJobOffer(int jobOfferId)
        {
            return Companies.Any(company => company.JobOffers.Any(jobOffer => jobOffer.Id == jobOfferId));
        }
    }
}