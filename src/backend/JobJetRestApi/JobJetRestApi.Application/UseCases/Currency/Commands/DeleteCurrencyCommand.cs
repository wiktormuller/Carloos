using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.Commands
{
    public class DeleteCurrencyCommand : IRequest
    {
        public int Id { get; private set; }
        
        public DeleteCurrencyCommand(int id)
        {
            Id = id;
        }
    }
}