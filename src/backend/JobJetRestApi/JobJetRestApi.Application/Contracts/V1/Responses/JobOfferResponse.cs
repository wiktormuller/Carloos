using System.Collections.Generic;
using JobJetRestApi.Domain.Enums;

namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class JobOfferResponse
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal SalaryFrom { get; private set; }
        public decimal SalaryTo { get; private set; }
        public AddressResponse Address { get; private set; }
        public List<string> TechnologyTypes { get; private set; }
        public string Seniority { get; private set; }
        public string EmploymentType { get; private set; }
        public WorkSpecification WorkSpecification { get; private set; }
        
        public int CompanyId { get; private set; }
        public string CompanyName { get; private set; }

        public JobOfferResponse(int id, string name, string description, decimal salaryFrom, decimal salaryTo, 
            string countryName, string town, string street, string zipCode, decimal latitude, decimal longitude,
            List<string> technologyTypes, string seniority, string employmentType, WorkSpecification workSpecification,
            int companyId, string companyName)
        {
            Id = id;
            Name = name;
            Description = description;
            SalaryFrom = salaryFrom;
            SalaryTo = salaryTo;
            TechnologyTypes = technologyTypes;
            Seniority = seniority;
            EmploymentType = employmentType;
            WorkSpecification = workSpecification;
            CompanyId = companyId;
            CompanyName = companyName;
            Address = new AddressResponse(countryName, town, street, zipCode, latitude, longitude);
        }
    }
}