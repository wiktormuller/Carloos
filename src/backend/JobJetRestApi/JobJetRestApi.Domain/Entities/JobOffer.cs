namespace JobJetRestApi.Domain.Entities
{
    public class JobOffer // Aggregate root
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal SalaryFrom { get; }
        public decimal SalaryTo { get; }
        public Address Address { get; }
        public TechnologyType TechnologyType { get; }
        public Seniority Seniority { get; }
        public EmploymentType EmploymentType { get; }
    }
}