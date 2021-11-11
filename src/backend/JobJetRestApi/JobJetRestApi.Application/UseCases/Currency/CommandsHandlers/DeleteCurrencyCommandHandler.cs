using System.Threading;
using System.Threading.Tasks;
using JobJetRestApi.Application.UseCases.Currency.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.CommandsHandlers
{
    public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand>
    {
        public DeleteCurrencyCommandHandler()
        {
            
        }

        public Task<Unit> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Unit.Value);
        }
    }
}