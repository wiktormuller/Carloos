using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.Currency.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.CommandsHandlers
{
    public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, int>
    {
        public CreateCurrencyCommandHandler()
        {
            
        }
        
        public Task<int> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currencyId = 123;
            
            return Task.FromResult(currencyId);
        }
    }
}