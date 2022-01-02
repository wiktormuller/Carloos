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
        public string TechnologyType { get; private set; }
        public string Seniority { get; private set; }
        public string EmploymentType { get; private set; }

        public JobOfferResponse(int id, string name, string description, decimal salaryFrom, decimal salaryTo, 
            int addressId, string countryName, string town, string zipCode, decimal latitude, decimal longitude,
            string technologyType, string seniority, string employmentType)
        {
            Id = id;
            Name = name;
            Description = description;
            SalaryFrom = salaryFrom;
            SalaryTo = salaryTo;
            TechnologyType = technologyType;
            Seniority = seniority;
            EmploymentType = employmentType;
            Address = new AddressResponse(addressId, countryName, town, zipCode, latitude, longitude);
        }
    }
}