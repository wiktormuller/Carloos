using MediatR;

namespace JobJetRestApi.Application.UseCases.Companies.Commands
{
    public class CreateCompanyCommand : IRequest<int>
    {
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public string Description { get; private set; }
        public int NumberOfPeople { get; private set; }
        public string CityName { get; set; }
        
        public CreateCompanyCommand(string name, string shortName, string description, int numberOfPeople, string cityName)
        {
            Name = name;
            ShortName = shortName;
            Description = description;
            NumberOfPeople = numberOfPeople;
            CityName = cityName;
        }
    }
}