namespace JobJetRestApi.Application.Contracts.V1.Responses
{
    public class CompanyResponse
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public string Description { get; private set; }
        public int NumberOfPeople { get; private set; }
        public string CityName { get; private set; }

        public CompanyResponse(int id, string name, string shortName, string description, int numberOfPeople, string cityName)
        {
            Id = id;
            Name = name;
            ShortName = shortName;
            Description = description;
            NumberOfPeople = numberOfPeople;
            CityName = cityName;
        }
    }
}