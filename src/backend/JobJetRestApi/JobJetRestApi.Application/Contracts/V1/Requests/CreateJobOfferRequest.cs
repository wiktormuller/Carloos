using JobJetRestApi.Application.Validators;

namespace JobJetRestApi.Application.Contracts.V1.Requests
{
    public class CreateJobOfferRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SalaryFrom { get; set; }
        public decimal SalaryTo { get; set; }
        public CreateAddressRequest Address { get; set; }
        public int TechnologyTypeId { get; set; }
        public int SeniorityId { get; set; }
        public int EmploymentTypeId { get; set; }
        public int CurrencyId { get; set; }
    }
}