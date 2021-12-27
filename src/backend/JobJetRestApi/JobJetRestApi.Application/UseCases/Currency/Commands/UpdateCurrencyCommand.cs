using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.Commands
{
    public class UpdateCurrencyCommand : IRequest
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        
        public UpdateCurrencyCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}