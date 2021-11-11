using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.Commands
{
    public class CreateCurrencyCommand : IRequest<int>
    {
        public CreateCurrencyCommand()
        {
            
        }
    }
}