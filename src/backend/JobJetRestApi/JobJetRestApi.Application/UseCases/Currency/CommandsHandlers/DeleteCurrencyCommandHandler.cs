using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.Currency.Commands;
using JobJetRestApi.Domain.Repositories;
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
            if (! await _currencyRepository.ExistsAsync(request.Id))
            {
                throw CurrencyNotFoundException.ForId(request.Id);
            }

            var currency = await _currencyRepository.GetByIdAsync(request.Id);
            
            await _currencyRepository.DeleteAsync(currency);
            
            return Unit.Value;
        }
    }
}