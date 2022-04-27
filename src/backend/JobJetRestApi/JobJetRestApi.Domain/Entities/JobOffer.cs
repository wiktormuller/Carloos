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
        
        public WorkSpecification WorkSpecification { get; private set; }
        
        // Relationships
        public int AddressId { get; set; } // Non nullable relationship
        public Address Address { get; private set; }
        
        public int TechnologyTypeId { get; set; } // Non nullable relationship
        public TechnologyType TechnologyType { get; private set; }
        
        public int SeniorityId { get; set; }
        public Seniority Seniority { get; private set; }
        
        public int EmploymentTypeId { get; set; }
        public EmploymentType EmploymentType { get; private set; }
        
        public int CurrencyId { get; set; }
        public Currency Currency { get; private set; }

        private JobOffer() {} // For EF purposes
        
        public JobOffer(string name,
            string description, 
            decimal salaryFrom,
            decimal salaryTo,
            Address address, 
            TechnologyType technologyType,
            Seniority seniority, 
            EmploymentType employmentType,
            Currency currency,
            WorkSpecification workSpecification)
        {
            Name = name;
            Description = description;
            SalaryFrom = salaryFrom;
            SalaryTo = salaryTo;
            Address = address;
            TechnologyType = technologyType;
            Seniority = seniority;
            EmploymentType = employmentType;
            Currency = currency;
            WorkSpecification = workSpecification;
        }

        public void UpdateBasicInformation(string name, string description, decimal salaryFrom, decimal salaryTo)
        {
            Name = Guard.Against.Null(name, nameof(name));
            Description = Guard.Against.Null(description, nameof(description));
            SalaryFrom = Guard.Against.Zero(salaryFrom, nameof(salaryFrom));
            SalaryTo = Guard.Against.Zero(salaryTo, nameof(salaryTo));
        }
    }
}