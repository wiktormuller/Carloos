using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using JobJetRestApi.Domain.Enums;

namespace JobJetRestApi.Domain.Entities
{
    public class JobOffer
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal SalaryFrom { get; private set; }
        public decimal SalaryTo { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        
        public WorkSpecification WorkSpecification { get; private set; }
        
        // Relationships
        public int AddressId { get; private set; } // Non nullable relationship
        public Address Address { get; private set; }
        public List<TechnologyType> TechnologyTypes { get; private set; }
        
        public int SeniorityId { get; private set; }
        public Seniority Seniority { get; private set; }
        
        public int EmploymentTypeId { get; private set; }
        public EmploymentType EmploymentType { get; private set; }
        
        public int CurrencyId { get; private set; }
        public Currency Currency { get; private set; }

        private JobOffer()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
        } // For EF purposes
        
        public JobOffer(string name,
            string description, 
            decimal salaryFrom,
            decimal salaryTo,
            Address address, 
            List<TechnologyType> technologyTypes,
            Seniority seniority, 
            EmploymentType employmentType,
            Currency currency,
            WorkSpecification workSpecification) : this()
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
            Description = Guard.Against.Null(description, nameof(description));
            SalaryFrom = Guard.Against.NegativeOrZero(salaryFrom, nameof(salaryFrom));
            SalaryTo = Guard.Against.NegativeOrZero(salaryTo, nameof(salaryTo));
            Address = Guard.Against.Null(address, nameof(address));
            TechnologyTypes = Guard.Against.Null(technologyTypes, nameof(technologyTypes));
            Seniority = Guard.Against.Null(seniority, nameof(seniority));
            EmploymentType = Guard.Against.Null(employmentType, nameof(employmentType));
            Currency = Guard.Against.Null(currency, nameof(currency));
            WorkSpecification = Guard.Against.EnumOutOfRange(workSpecification, nameof(workSpecification));
        }

        public void UpdateBasicInformation(string name, string description, decimal salaryFrom, decimal salaryTo)
        {
            Name = Guard.Against.Null(name, nameof(name));
            Description = Guard.Against.Null(description, nameof(description));
            SalaryFrom = Guard.Against.Zero(salaryFrom, nameof(salaryFrom));
            SalaryTo = Guard.Against.Zero(salaryTo, nameof(salaryTo));

            UpdatedAt = DateTime.UtcNow;
        }
    }
}