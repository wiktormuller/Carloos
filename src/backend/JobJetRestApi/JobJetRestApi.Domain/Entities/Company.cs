namespace JobJetRestApi.Domain.Entities
{
    public class Company
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public string Description { get; private set; }
        public int NumberOfPeople { get; private set; }
        public string CityName { get; private set; } // Maybe other object?

        private Company() {}

        public Company(string name, string shortName, string description, int numberOfPeople, string cityName)
        {
            Name = name;
            ShortName = shortName;
            Description = description;
            NumberOfPeople = numberOfPeople;
            CityName = cityName;
        }

        public void Update(string name, string shortName, string description, int numberOfPeople, string cityName)
        {
            Name = name;
            ShortName = shortName;
            Description = description;
            NumberOfPeople = numberOfPeople;
            CityName = cityName;
        }
    }
}