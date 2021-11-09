namespace JobJetRestApi.Domain.Entities
{
    public class JobOffer // Aggregate root
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal SalaryFrom { get; private set; }
        public decimal SalaryTo { get; private set; }
        public Address Address { get; private set; }
        public TechnologyType TechnologyType { get; private set; }
        public Seniority Seniority { get; private set; }
        public EmploymentType EmploymentType { get; private set; }
    }
}