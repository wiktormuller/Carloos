using JobJetRestApi.Application.Contracts.V1.Responses;

namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class JobOfferResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SalaryFrom { get; set; }
        public decimal SalaryTo { get; set; }
        public AddressResponse Address { get; set; }
        public string TechnologyType { get; set; }
        public string Seniority { get; set; }
        public string EmploymentType { get; set; }
        
        // @TODO - Implement constructor
    }
}