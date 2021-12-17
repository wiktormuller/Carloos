namespace JobJetRestApi.Application.Contracts.V1.Requests
{
    public class UpdateJobOfferRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SalaryFrom { get; set; }
        public decimal SalaryTo { get; set; }
    }
}