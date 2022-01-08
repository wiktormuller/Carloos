using MediatR;

namespace JobJetRestApi.Application.UseCases.Companies.Commands
{
    public class UpdateCompanyCommand : IRequest
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public string Description { get; private set; }
        public int NumberOfPeople { get; private set; }
        public string CityName { get; set; }
        
        public UpdateCompanyCommand(int id, string name, string shortName, string description, int numberOfPeople, string cityName)
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