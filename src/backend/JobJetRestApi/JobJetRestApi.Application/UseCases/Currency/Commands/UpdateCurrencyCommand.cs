using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.Commands
{
    public class UpdateCurrencyCommand : IRequest
    {
        public int Id { get; private set; }
        
        public UpdateCurrencyCommand(int id)
        {
            Id = id;
        }
    }
}