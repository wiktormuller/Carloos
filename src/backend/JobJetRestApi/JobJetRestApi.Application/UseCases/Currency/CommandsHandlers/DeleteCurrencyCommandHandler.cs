using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Application.UseCases.Currency.Commands;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Currency.CommandsHandlers
{
    public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand>
    {
        private readonly ICurrencyRepository _currencyRepository;
        
        public DeleteCurrencyCommandHandler(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = Guard.Against.Null(currencyRepository, nameof(currencyRepository));
        }

        public async Task<Unit> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            if (! await _currencyRepository.Exists(request.Id))
            {
                throw CurrencyNotFoundException.ForId(request.Id);
            }

            var currency = await _currencyRepository.GetById(request.Id);
            
            await _currencyRepository.Delete(currency);
            
            return Unit.Value;
        }
    }
}