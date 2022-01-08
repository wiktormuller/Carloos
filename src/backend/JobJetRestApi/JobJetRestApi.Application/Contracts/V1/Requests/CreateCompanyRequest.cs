namespace JobJetRestApi.Application.Contracts.V1.Requests
{
    public class CreateCompanyRequest
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public int NumberOfPeople { get; set; }
        public string CityName { get; set; }
    }
}