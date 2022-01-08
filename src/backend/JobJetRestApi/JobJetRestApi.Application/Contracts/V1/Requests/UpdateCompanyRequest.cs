namespace JobJetRestApi.Application.Contracts.V1.Requests
{
    public class UpdateCompanyRequest
    {
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public string Description { get; private set; }
        public int NumberOfPeople { get; private set; }
        public string CityName { get; private set; }

        public UpdateCompanyRequest(string name, string shortName, string description, int numberOfPeople, string cityName)
        {
            Name = name;
            ShortName = shortName;
            Description = description;
            NumberOfPeople = numberOfPeople;
            CityName = cityName;
        }
    }
}