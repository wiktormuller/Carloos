using MediatR;

namespace JobJetRestApi.Application.UseCases.Companies.Commands
{
    public class UpdateCompanyCommand : IRequest
    {
        public int UserId { get; private set; }
        public int Id { get; private set; }
        public string Description { get; private set; }
        public int NumberOfPeople { get; private set; }
        
        public UpdateCompanyCommand(int userId, int id, string description, int numberOfPeople)
        {
            UserId = userId;
            Id = id;
            Description = description;
            NumberOfPeople = numberOfPeople;
        }
    }
}