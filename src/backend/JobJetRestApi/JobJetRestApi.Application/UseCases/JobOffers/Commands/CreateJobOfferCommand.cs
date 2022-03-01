﻿using MediatR;

namespace JobJetRestApi.Application.UseCases.JobOffers.Commands
{
    public class CreateJobOfferCommand : IRequest<int>
    {
        public string Name { get; }
        public string Description { get; }
        public decimal SalaryFrom { get; }
        public decimal SalaryTo { get; }
        public int TechnologyTypeId { get; }
        public int SeniorityId { get; }
        public int EmploymentTypeId { get; }
        public int CurrencyId { get; }
        public string Town { get; }
        public string Street { get; }
        public string ZipCode { get; }
        public int CountryId { get; }
        public string WorkSpecification { get; }

        public CreateJobOfferCommand(
            string name, 
            string description, 
            decimal salaryFrom, 
            decimal salaryTo, 
            int technologyTypeId, 
            int seniorityId,
            int employmentTypeId,
            string town,
            string street,
            string zipCode,
            int countryId,
            int currencyId,
            string workSpecification)
        {
            Name = name;
            Description = description;
            SalaryFrom = salaryFrom;
            SalaryTo = salaryTo;
            TechnologyTypeId = technologyTypeId;
            SeniorityId = seniorityId;
            EmploymentTypeId = employmentTypeId;
            Town = town;
            Street = street;
            ZipCode = zipCode;
            CountryId = countryId;
            CurrencyId = currencyId;
            WorkSpecification = workSpecification;
        }
    }
}